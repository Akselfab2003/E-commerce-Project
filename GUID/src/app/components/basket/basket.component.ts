import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { Products } from '../../models/Products';
import { BasketDetails } from '../../models/BasketDetails';
import { Basket } from '../../models/Basket';
import { basketLogic } from '../../logic/basketLogic';
import { ProductVariants } from '../../models/ProductVariants';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css',
  animations:[
    trigger("BasketAnimation",[
        state("Open",style({
          "margin-right":"0%"
         })),
         state("Closed",style({
          "margin-right":"-60%"
         })),
         transition("Open => Closed",
         animate("0.800s ease-in")
         ),
         transition("Closed => Open",
         animate("0.800s ease-out")
         )
      ]
    )
  ]
})
export class BasketComponent<T> {
  public BasketState:string = "Closed";
  public BasketStateBool:boolean = false;

  basket: Basket = new Basket();
  subtotal:number = 0;

  constructor(private basketItems:basketLogic<T>)
  {
     basketItems.AddToBasketEvent.subscribe(ele => {this.GetBasket()})
  }

  GetBasket() {
    this.basketItems.GetBasket().subscribe(res => {
      this.basket = res;
      this.subtotal = this.calculateTotal();
      console.log("Your basket:",res)
    });
  };

  GetProductVariant(){

  }
  calculateTotal(): number {
    return this.basketItems.basketDetails.reduce((total, item) => 

      total + (item.quantity * (item.products == undefined ? item.variant : item.products).price), 0);
    

  } 
  ngOnInit(){
    this.GetBasket();
  }

  getUniqueProducts(): BasketDetails[] {
  
    return this.basketItems.basketDetails;
  }

  getUniqueBasketDetails():BasketDetails[]{
    const uniqueBasketDetails: BasketDetails[] = [];

    const uniqueBasketDetailIds = new Set<number>();
  
    this.basket.basketDetails.forEach ((item) => {
      const basketId = item.id;
      if (!uniqueBasketDetailIds.has(basketId)) {
        uniqueBasketDetailIds.add(basketId);
        uniqueBasketDetails.push(item);
      }
    });

    return uniqueBasketDetails;
  }

  AddProductUsingQtyClick(product?:Products,variant?:ProductVariants){
    var basketDetailId = this.FindBasketDetailId(product, variant);
    console.log("BasketDetailId =", basketDetailId)
    var basketDetailObject = this.basket.basketDetails.find(detail => detail.id == basketDetailId)
    basketDetailObject != undefined ?  basketDetailObject.quantity = basketDetailObject.quantity + 1 : null;
    this.basketItems.UpdateBasket(this.basket )
  }

  SubtractProductUsingQtyClick(product?:Products,variant?:ProductVariants){

    var basketDetailId = this.FindBasketDetailId(product, variant);
    var basketDetailObject = this.basket.basketDetails.find(detail => detail.id == basketDetailId)
    if(basketDetailObject != undefined ){

        if(basketDetailObject.quantity > 1 ){
          basketDetailObject.quantity = basketDetailObject.quantity - 1 ;
          this.basketItems.UpdateBasket(this.basket)

        }
        else{
         this.RemoveProductFromBasket(basketDetailObject)
        }

    }
  }

  FindBasketDetailId(product?:Products,variant?:ProductVariants):number{
  //Wont be able to find Variant Id and therefore not able to add or subtract using the - and + signs?
  
  //return this.basket.basketDetails.find(detail => detail.products.id == product.id)?.id -- original working line (without variants)

    /* var test = this.basket.basketDetails.find(detail =>{
      //console.log(detail.products.id), console.log(product?.id), console.log("Variant Id",variant?.id)
      if(product != null){
         (detail.products.id == product.id) ? detail.id : 1
      }
      else if(variant != undefined && detail.variant != undefined)
      {
        //console.log("DETAIL VARIANT ID",detail.variant),
        (detail.variant.id == variant.id) ? detail.id : 1
      }
      console.log(detail);
    })
    console.log("FIND BASKET DETAIL TEST",test);
    return test != undefined ?test.id:0; */

    var value:number;
    if(product != undefined && this.basket.basketDetails != undefined){
      value = (this.basket.basketDetails.filter(ele => ele.products != null).find(ele => ele.products == product)?.id) != undefined ? (this.basket.basketDetails.filter(ele => ele.products != null).find(ele => ele.products.id == product.id))?.id : 0
      return value;
    }
    else{
      return 1
    }
  }

  RemoveProductFromBasket(basketDetails:BasketDetails){
    this.basketItems.RemoveFromBasket(basketDetails);
  }

  ChangeState() {
    this.BasketStateBool = !this.BasketStateBool;
    this.BasketState = this.BasketStateBool ? "Open" : "Closed";
    if(this.BasketState == "Open"){
      this.GetBasket();
    }
  }

  closeBasket(){
    if(this.BasketState == "Open"){
      this.ChangeState();
    }
  }
}
