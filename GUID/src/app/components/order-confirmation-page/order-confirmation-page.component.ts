import { Component } from '@angular/core';
import { sessionController } from '../../logic/sessionLogic';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Order } from '../../models/Order';
import { orderDetails } from '../../models/orderDetails';

@Component({
  selector: 'app-order-confirmation-page',
  templateUrl: './order-confirmation-page.component.html',
  styleUrl: './order-confirmation-page.component.css'
})
export class OrderConfirmationPageComponent<T> {
  constructor(private service: HttpserviceService<T>) {
    
  }
  public order: Order = new Order();
  //public orderLines:orderDetails[] = this.order.orderLines

  GetOrder(){
    console.log("getting order");
    var sessionId:string = sessionController.GetCookie();
        this.service.GetRequest<Order[]>(`Orders/${sessionId}`).subscribe(orderInfo =>{
          this.order = orderInfo[0];
          console.log("Your Order:", {orderInfo});
          console.log("ORDER OBJECT:", this.order);
        })

        
        return this.service.GetRequest<Order>(`Orders/${sessionId}`)
  }



  ngOnInit(){
    this.GetOrder();
  }
}
