import { Component } from '@angular/core';
import { sessionController } from '../../logic/sessionLogic';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Order } from '../../models/Order';
import { orderDetails } from '../../models/orderDetails';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs/internal/operators/tap';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-order-confirmation-page',
  templateUrl: './order-confirmation-page.component.html',
  styleUrl: './order-confirmation-page.component.css'
})
export class OrderConfirmationPageComponent<T> {
  constructor(private service: HttpserviceService<T>, private route: ActivatedRoute) {
    
  }
  public order: Order = new Order();

  GetOrder(sessId:String){
    console.log("getting order");
        this.service.GetRequest<Order>(`Orders/GetSingularOrder/${sessId}`).subscribe(orderInfo =>{
          this.order = orderInfo;
          for (let i = 0; i < this.order.orderLines.length; i++) {
            console.log(this.order.orderLines[i].quantity)
            
          }
        })
  }

  ngOnInit(){
    this.route.paramMap.subscribe((data)=>{
      this.GetOrder(String(data.get('sessId')));
       
    })
  }
}
