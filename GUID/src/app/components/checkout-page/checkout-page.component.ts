import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Products } from '../../models/Products';
import { Order } from '../../models/Order';
import { sessionController } from '../../logic/sessionLogic';
import { Session } from '../../models/Session';
import { Basket } from '../../models/Basket';

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

  constructor(private service:HttpserviceService<T>) { };

  billingDetails = {
    fullName: '',
    email: '',
    address: '',
  };

  ngOnInit(): void {
    this.GetBakset();
  };

  GetBakset(){
    let sessid:string=sessionController.GetCookie();

    this.service.GetRequest<Basket>("Basket/").subscribe((data)=>{
      this.basketItems = data;
      console.log(this.basketItems)
    });
  }

  /* calculateTotal(): number {
    return this.basketItems.reduce((total, item) => total + this.products.price, 0);
  } */

  placeOrder(): void {
    // Handle the order placement logic, e.g., send data to the server
    console.log('Placing order:', this.billingDetails, 'Items:', this.GetBakset());
  }
}
