import { ActivatedRoute } from "@angular/router";
import { BasketDetails } from "../models/BasketDetails";
import { Basket } from "../models/Basket";
import { HttpserviceService } from "../../Services/httpservice.service";
import { sessionController } from "./sessionLogic";
import { Injectable } from "@angular/core";
import { Products } from "../models/Products";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  
})
export class basketLogic<T> {
    
    constructor(private route: ActivatedRoute, private service: HttpserviceService<T>) {}

    public primaryBasket: Basket = new Basket();
    public basketDetails: BasketDetails[] = new Array<BasketDetails>;
  
    GetBasket():Observable<Basket>{
        var sessionId:string = sessionController.GetCookie();
        return this.service.GetRequest<Basket>(`Basket/GetBasket/${sessionId}`)
    };
  
    public AddToBasket(product:Products){
        var sessionId:string = sessionController.GetCookie();
        var newBasketDetails:BasketDetails = new BasketDetails();
        newBasketDetails.products = product;
        this.service.PostRequest<BasketDetails[]>(`Basket/AddToBasket/${sessionId}`, newBasketDetails).subscribe((data)=>{
        this.basketDetails = data;
        console.log(data);
      });
    };
  
    RemoveFromCart(Id:number){
      this.service.DeleteRequest<BasketDetails[]>(`BasketDetails/${Id}`).subscribe((data)=>{
        this.basketDetails = data
      });
    };
  
    GetTotalPrice(){
      this.service.GetRequest<Basket>(`BasketDetails`)
    };
}