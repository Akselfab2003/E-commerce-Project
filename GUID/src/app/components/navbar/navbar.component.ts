import { Component, ViewChild } from '@angular/core';
import { BasketComponent } from '../basket/basket.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  @ViewChild(BasketComponent)
  private Basket!: BasketComponent; 
  
  ngOnInit(){
  }

  changeBasket(){
    this.Basket.ChangeState()
    console.log("Tse")
  }
}
