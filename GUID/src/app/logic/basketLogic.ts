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
    public basketDetails: BasketDetails[] = this.primaryBasket.basketDetails;
  
    GetBasket():Observable<Basket>{
        var sessionId:string = sessionController.GetCookie();
        this.service.GetRequest<Basket>(`Basket/GetBasket/${sessionId}`).subscribe(basket =>{this.primaryBasket = basket
          console.log(this.primaryBasket);
        })
        return this.service.GetRequest<Basket>(`Basket/GetBasket/${sessionId}`)
    };
  
    public AddToBasket(product:Products){
        var sessionId:string = sessionController.GetCookie();
        var newBasketDetails:BasketDetails = new BasketDetails();
        newBasketDetails.products = product;
        if(this.basketDetails.find(detail => detail.products.id == product.id)!= undefined){
          var basketDetailObject = this.basketDetails.find(detail => detail.products.id == product.id)
          basketDetailObject != undefined ?  basketDetailObject.quantity = basketDetailObject.quantity + 1 : null;
          this.UpdateBasket();
        }
        else{
          this.service.PostRequest<BasketDetails[]>(`Basket/AddToBasket/${sessionId}`, newBasketDetails).subscribe((data)=>{
          });
        }

    };

    public RemoveFromBasket(basketDetails:BasketDetails){
      var sessionId:string = sessionController.GetCookie();
      this.service.PostRequest<BasketDetails[]>(`Basket/RemoveFromBasket/${sessionId}`, basketDetails).subscribe((data)=>{
        this.basketDetails = data;
        console.log(data);
      });
    }
  
    UpdateBasket(){
      this.service.PutRequest<any>(`Basket/${this.primaryBasket.id}`, this.primaryBasket).subscribe((data)=>{
        
      });
    }

    GetTotalPrice(){
      this.service.GetRequest<Basket>(`BasketDetails`)
    };
}