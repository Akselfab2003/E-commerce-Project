import { ActivatedRoute } from "@angular/router";
import { BasketDetails } from "../models/BasketDetails";
import { Basket } from "../models/Basket";
import { HttpserviceService } from "../../Services/httpservice.service";
import { sessionController } from "./sessionLogic";
import { EventEmitter, Injectable, Output } from "@angular/core";
import { Products } from "../models/Products";
import { Observable } from "rxjs";
import { ProductVariants } from "../models/ProductVariants";

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
        })
        
        
        return   this.service.GetRequest<Basket>(`Basket/GetBasket/${sessionId}`)

       
    };
    
   
    public AddVariantBasket(VaraintDetail:BasketDetails){
      this.GetBasket().subscribe(data => {
        var variant = data.basketDetails.find(ele => ele.variant.id == VaraintDetail.variant.id);
        if(variant != null)
        {
          
          variant != undefined ? variant.quantity = variant.quantity+VaraintDetail.quantity: null;
          this.UpdateBasket(data);

        }
        else{
          this.AddBasketDetail(VaraintDetail);
        }
      });
    }


    public AddProductToBasket(ProductDetail:BasketDetails){
      this.GetBasket().subscribe(data => {
        var productFromBasketDetails = data.basketDetails.find(ele => ele.products.id == ProductDetail.products.id);
        if(productFromBasketDetails != null)
        {
          productFromBasketDetails.variant = new ProductVariants();

          productFromBasketDetails != undefined ? productFromBasketDetails.quantity = productFromBasketDetails.quantity+ProductDetail.quantity: null;
          this.UpdateBasket(data);

        }
        else{
          
          this.AddBasketDetail(ProductDetail);
        }
      });
    }



    public AddToBasket(Product?:Products,Variant?:ProductVariants,Quantity?:number):Promise<any>{
        var newBasketDetails:BasketDetails = new BasketDetails();
        newBasketDetails.products = Product != undefined ? Product : new Products() ;
        newBasketDetails.variant  = Variant != undefined ? Variant : new ProductVariants() ;
        if(Product != undefined){

          newBasketDetails.quantity = (Quantity !=undefined && Quantity != 0) ? Quantity : Product.Quantity
          this.AddProductToBasket(newBasketDetails)
        }
        else{

          newBasketDetails.quantity = (Quantity !=undefined && Quantity != 0) ? Quantity : 1
          this.AddVariantBasket(newBasketDetails);

        }
        return new Promise(resolve =>{
          resolve("Success");
        })
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
        console.log("Your Basket",basket)
        this.service.PutRequest<any>(`Basket/${basket.id}`,basket).subscribe((data)=>{
          console.log(data)
        this.AddToBasketEvent.emit()
      });
    }


    AddBasketDetail(detail:BasketDetails){ 

      var sessionId:string = sessionController.GetCookie();

      this.service.PostRequest<BasketDetails[]>(`Basket/AddToBasket/${sessionId}`, detail).subscribe((data)=>{        
      this.AddToBasketEvent.emit()
    });
  }

}