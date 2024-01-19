import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { BasketComponent } from '../basket/basket.component';
import { adminGuard } from '../../logic/admin.guard';
import { adminController } from '../../logic/adminLogic';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  @ViewChild(BasketComponent)  private Basket!: BasketComponent<any>; 
  @Output() TagsChangedEvent = new EventEmitter<boolean>()
  public theme:boolean = true;
  ngOnInit(){
  }
  changeTheme(){
    this.theme=!this.theme
    this.TagsChangedEvent.emit(this.theme)
  }
  changeBasket(){
    this.Basket.ChangeState()
    console.log("Tse")
  }
  canShowLink():boolean{
    return adminController.hasRequiredRole();
  }
}
