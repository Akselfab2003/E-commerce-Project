import { Component } from '@angular/core';
import { User } from '../../models/User';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Order } from '../../models/Order';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent<T> {

  user:User = new User();
  order:Order = new Order();

  constructor(private service:HttpserviceService<T>) { };


  ngOnInit(): void {
    this.GetUser();
    this.GetOrders();
  };

  GetUser(){
    this.service.GetRequest<User>("User/test").subscribe((data)=>{
      this.user = data;
      console.log(this.user)
    });
  }

  GetOrders(){
    this.service.GetRequest<Order>("api/Orders").subscribe((data)=>{
      this.order = data;
      console.log(this.order)
    });
  }
}
