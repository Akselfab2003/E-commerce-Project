import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Products } from '../../models/Products';
import { Order } from '../../models/Order';
import { sessionController } from '../../logic/sessionLogic';
import { Session } from '../../models/Session';
import { Basket } from '../../models/Basket';
import { basketLogic } from '../../logic/basketLogic';

@Component({
  selector: 'app-checkout-page',
  templateUrl: './checkout-page.component.html',
  styleUrl: './checkout-page.component.css'
})
export class CheckoutPageComponent <T> {

  basketItems:Basket = new Basket();
  public products: Products = new Products();
  orders:Order[] = new Array<Order>();
  session:Session = new Session();

  constructor(private service:HttpserviceService<T>, private basketTest:basketLogic<T>) 
  {

    basketTest.AddToBasketEvent.subscribe(ele => {this.GetBasket()})

   };

  billingDetails = {
    fullName: '',
    email: '',
    address: '',
  };

  ngOnInit(): void {
    this.GetBasket();
  };

  GetBasket(){
    
    this.basketTest.GetBasket().subscribe(res => this.basketItems = res)
  }

  calculateTotal(): number {
    return this.basketItems.basketDetails.reduce((total, item) => total + item.products.price, 0);
  } 

  placeOrder(): void {
    // Handle the order placement logic, e.g., send data to the server
    console.log('Placing order:', this.billingDetails, 'Items:', this.basketItems, this.GetBasket());
  }
}
