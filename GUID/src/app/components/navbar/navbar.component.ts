import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { BasketComponent } from '../basket/basket.component';
import { adminGuard } from '../../logic/admin.guard';
import { adminController } from '../../logic/adminLogic';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { sessionController } from '../../logic/sessionLogic';
import { HttpserviceService } from '../../../Services/httpservice.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  
  @ViewChild(BasketComponent)  private Basket!: BasketComponent<any>; 
  @Output() TagsChangedEvent = new EventEmitter<boolean>()
  
  constructor(private router:Router, private httpService:HttpserviceService<any>, private route: Router){
  }

  public SearchForm:FormGroup = new FormGroup({
    SearchInput: new FormControl<string>("",Validators.required)
  })

  public isLogedin:boolean = false;
  public theme:boolean = true;

  ngOnInit(){
    sessionController.LoginState.subscribe((data)=>{
      this.isLogedin = data
    });
  };

  changeTheme(){
    this.theme=!this.theme
    this.TagsChangedEvent.emit(this.theme)

  }
  changeBasket(){
    this.Basket.ChangeState()
  }
  Search(){
    var Input:string = this.SearchForm.get("SearchInput")?.value
    this.router.navigate(["/Search",Input])
  }

  Logout(){
    if(this.isLogedin == true){
      sessionController.CreateEmptySession(this.httpService);
      this.isLogedin = false
    }
    this.router.navigateByUrl("/home-page");

  }
}
