import { Component } from '@angular/core';
import { User } from '../../models/User';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Order } from '../../models/Order';
import { sessionController } from '../../logic/sessionLogic';
import { Session } from '../../models/Session';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent<T> {

  user:User = new User();
  order:Order[] = new Array<Order>();
  session:Session = new Session();

  constructor(private service:HttpserviceService<T>) { };


  ngOnInit(): void {
    this.GetUser();
    this.GetOrders();
  };

  GetUser(){
    let sessid=sessionController.GetCookie();
    this.service.GetRequest<Session>("User/SessionId"+sessid).subscribe((data)=>{
      this.session = data;
      this.user=this.session.user || new User();
      console.log(this.user)
    });
  }

  GetOrders(){
    this.service.GetRequest<Order>("Orders/1").subscribe((data)=>{
      this.order = data;
      console.log(this.order)
    });
  }
}
