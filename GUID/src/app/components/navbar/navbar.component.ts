import { Component, ViewChild } from '@angular/core';
import { BasketComponent } from '../basket/basket.component';
import { adminGuard } from '../../logic/admin.guard';
import { adminController } from '../../logic/adminLogic';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  @ViewChild(BasketComponent)
  private Basket!: BasketComponent<any>; 
  
  ngOnInit(){
  }

  changeBasket(){
    this.Basket.ChangeState()
    console.log("Tse")
  }
  canShowLink():boolean{
    return adminController.hasRequiredRole();
  }
}
