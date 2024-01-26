import { ActivatedRoute } from "@angular/router";
import { BasketDetails } from "../models/BasketDetails";
import { Basket } from "../models/Basket";
import { HttpserviceService } from "../../Services/httpservice.service";
import { sessionController } from "./sessionLogic";
import { EventEmitter, Injectable, Output } from "@angular/core";
import { Products } from "../models/Products";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  
})
export class basketLogic<T> {
    
    constructor(private route: ActivatedRoute, private service: HttpserviceService<T>) {
    
    }

    @Output() AddToBasketEvent:EventEmitter<T>= new EventEmitter();
    public primaryBasket: Basket = new Basket();
    public basketDetails: BasketDetails[] = this.primaryBasket.basketDetails;
  
    GetBasket(): Observable<Basket>{
        var sessionId:string = sessionController.GetCookie();
        this.service.GetRequest<Basket>(`Basket/GetBasket/${sessionId}`).subscribe(basket =>{
          this.primaryBasket = basket
          console.log(basket);
        })
        
        
        return   this.service.GetRequest<Basket>(`Basket/GetBasket/${sessionId}`)

       
    };
    
   


    public AddToBasket(product:Products){
        var sessionId:string = sessionController.GetCookie();
        var newBasketDetails:BasketDetails = new BasketDetails();
        newBasketDetails.products = product;
        console.log(this.primaryBasket.basketDetails.find(detail => detail.products.id == product.id))
        if(this.primaryBasket.basketDetails.find(detail => detail.products.id == product.id)!= undefined){
          var basketDetailObject = this.primaryBasket.basketDetails.find(detail => detail.products.id == product.id)
          basketDetailObject != undefined ?  basketDetailObject.quantity = basketDetailObject.quantity + 1 : null;
          this.UpdateBasket(this.primaryBasket);
       

          this.AddToBasketEvent.emit()

        }
        else{
          this.service.PostRequest<BasketDetails[]>(`Basket/AddToBasket/${sessionId}`, newBasketDetails).subscribe((data)=>{
            
            this.AddToBasketEvent.emit()
          });

        }

    };

    public RemoveFromBasket(basketDetails:BasketDetails){
      var sessionId:string = sessionController.GetCookie();
      this.service.PostRequest<BasketDetails[]>(`Basket/RemoveFromBasket/${sessionId}`, basketDetails).subscribe((data)=>{
        this.basketDetails = data;
        console.log(data);
        
        this.AddToBasketEvent.emit()

      });
    }
  
    UpdateBasket(basket:Basket){
      this.service.PutRequest<any>(`Basket/${basket.id}`,basket).subscribe((data)=>{
       
      });
    }

    GetTotalPrice(){
      this.service.GetRequest<Basket>(`BasketDetails`)
    };
}