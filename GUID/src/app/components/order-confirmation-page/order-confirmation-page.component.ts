import { Component } from '@angular/core';
import { sessionController } from '../../logic/sessionLogic';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Order } from '../../models/Order';
import { orderDetails } from '../../models/orderDetails';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-confirmation-page',
  templateUrl: './order-confirmation-page.component.html',
  styleUrl: './order-confirmation-page.component.css'
})
export class OrderConfirmationPageComponent<T> {
  constructor(private service: HttpserviceService<T>, private route: ActivatedRoute) {
    
  }
  public order: Order = new Order();
  //public orderLines:orderDetails[] = this.order.orderLines

  GetOrder(sessId:String){
    console.log("getting order");
        this.service.GetRequest<Order[]>(`Orders/${sessId}`).subscribe(orderInfo =>{
          this.order = orderInfo[0];
          console.log("Your Order:", {orderInfo});
          console.log("ORDER OBJECT:", this.order);
        })
  }



  ngOnInit(){
    this.route.paramMap.subscribe((data)=>{
      this.GetOrder(String(data.get('sessId')));
       
    })
    
  }
}
