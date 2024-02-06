import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Products } from '../../models/Products';
import {  Order } from '../../models/Order';
import { sessionController } from '../../logic/sessionLogic';
import { Session } from '../../models/Session';
import { Basket } from '../../models/Basket';
import { basketLogic } from '../../logic/basketLogic';
import { orderDetails } from '../../models/orderDetails';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from '../../models/User';
import { Router } from '@angular/router';

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

  constructor(private service:HttpserviceService<T>, private basketTest:basketLogic<T>, private router:Router) 
  {

    basketTest.AddToBasketEvent.subscribe(ele => {this.GetBasket()})

   };
  UserInfo!:FormGroup


  billingDetails = {
    fullName: '',
    email: '',
    address: '',
  };

  ngOnInit(): void {
    this.GetBasket();
   this.UserInfo = new FormGroup({
      fullName: new FormControl<string>("",Validators.required),
      email: new FormControl<string>("",Validators.required),
      address: new FormControl<string>("",Validators.required),
    })
  
  };

  GetBasket(){
    
    this.basketTest.GetBasket().subscribe(res => this.basketItems = res)
  }

  FillUpOrderObjectWithUserInput():Order{
    var order:Order = new Order();
    order.fullname = this.UserInfo.get("fullName")?.value;
    order.email =  this.UserInfo.get("email")?.value;
    order.address = this.UserInfo.get("address")?.value;
    var test =  this.basketItems.basketDetails
    return order
  }

  GenerateOrder(){
    var order:Order = this.FillUpOrderObjectWithUserInput();
    console.log(this.basketItems.basketDetails)
    this.basketItems.basketDetails.forEach(ele => {
      var currentElement = ele.products == undefined ? ele.variant : ele.products;

      order.orderLines?.push({id:0,product:(ele.products != undefined ? ele.products :undefined ),variant:(ele.variant != undefined ? ele.variant :undefined ),price:currentElement.price,quantity:ele.quantity,total:(currentElement.price*ele.quantity)})
    })

    order.total =  this.calculateTotal()

    order.users = new User();
    console.log(order)

    console.log(JSON.stringify(order))
    this.placeOrder(order);
  }

  

  calculateTotal(): number {
    return this.basketItems.basketDetails.reduce((total, item) => 

      total + (item.quantity * (item.products == undefined ? item.variant : item.products).price), 0);
    

  } 

  placeOrder(order:Order): void {
    this.service.PostRequest<Session|null>(`Orders?sessid=${sessionController.GetCookie()}`,order).subscribe((Data) => {
      var currentSessId = Data?.sessId;
      if(Data != null){
        sessionController.SetCookie(Data)
      }

      console.log(Data)
      this.router.navigate(["/order-confirmation-page", currentSessId]);
    })
  }
}
