import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { BasketComponent } from '../basket/basket.component';
import { adminGuard } from '../../logic/admin.guard';
import { adminController } from '../../logic/adminLogic';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  
  @ViewChild(BasketComponent)  private Basket!: BasketComponent<any>; 
  @Output() TagsChangedEvent = new EventEmitter<boolean>()
  
  constructor(private router:Router){
  }

  public SearchForm:FormGroup = new FormGroup({
    SearchInput: new FormControl<string>("",Validators.required)
  })

  public isLogedin:boolean = false;
  public theme:boolean = true;

  ngOnInit(){}
  changeTheme(){
    this.theme=!this.theme
    this.TagsChangedEvent.emit(this.theme)
  }
  changeBasket(){
    this.Basket.ChangeState()
    console.log("Tse")
  }
  Search(){
    var Input:string = this.SearchForm.get("SearchInput")?.value
    console.log(Input)
    this.router.navigate(["/Search",Input])
  }

  Logout(){
    if(this.isLogedin == true){
      adminController.Logout()
      this.isLogedin = false
      this.router.navigateByUrl("/Login")
    }
  }
}
