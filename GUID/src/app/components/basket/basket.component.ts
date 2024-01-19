import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Products } from '../../models/Products';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscriber } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { BasketDetails } from '../../models/BasketDetails';
import { Basket } from '../../models/Basket';
import { basketLogic } from '../../logic/basketLogic';

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
  public BasketState:string = "Closed"
  public BasketStateBool:boolean = false

  basket: Basket = new Basket();
  constructor(private basketItems:basketLogic<T>)
  {

  }
  
  GetBasket() {
    this.basketItems.GetBasket().subscribe(res => this.basket = res)
  };

  RemoveProduct(){
    this.basketItems.RemoveFromCart(this.basket.id);
  }

  calculateTotal(): number {
    return this.basketItems.basketDetails.reduce((total, item) => total + item.products.price, 0);
  } 


  getUniqueProducts(): Products[] {
    const uniqueProducts: Products[] = [];

    const uniqueProductIds = new Set<number>();
  
    this.basketItems.basketDetails.forEach((item) => {
      const productId = item.products.id;
      if (!uniqueProductIds.has(productId)) {
        uniqueProductIds.add(productId);
        uniqueProducts.push(item.products);
      }
    });

    return uniqueProducts;
  }

  calculateProductCounts(productId: number): number {
    return this.basketItems.basketDetails.reduce((count, item) => {
      return item.products.id === productId ? count + 1 : count;
    }, 0);
  }

  ChangeState() {
    this.BasketStateBool = !this.BasketStateBool
    this.BasketState = this.BasketStateBool ? "Open" : "Closed"
    if(this.BasketState == "Open"){
      this.GetBasket();
    }
  }

  closeBasket(){
    if(this.BasketState == "Open"){
      this.ChangeState()
    }
  }
}
