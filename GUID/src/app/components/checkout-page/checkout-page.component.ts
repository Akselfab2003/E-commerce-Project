import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Products } from '../../models/Products';
import { Order } from '../../models/Order';
import { sessionController } from '../../logic/sessionLogic';
import { Session } from '../../models/Session';

@Component({
  selector: 'app-checkout-page',
  templateUrl: './checkout-page.component.html',
  styleUrl: './checkout-page.component.css'
})
export class CheckoutPageComponent <T> {

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
    this.GetOrders();
  };

  GetOrders(){
    let sessid:string=sessionController.GetCookie();

    this.service.GetRequest<Order[]>("Orders/"+sessid).subscribe((data)=>{
      this.orders = data;
      console.log(this.orders)
    });
  }

  /* calculateTotal(): number {
    return this.GetProduct().reduce((total, item) => total + item.price, 0);
  } */

  placeOrder(): void {
    // Handle the order placement logic, e.g., send data to the server
    console.log('Placing order:', this.billingDetails, 'Items:', this.GetOrders());
  }
}
