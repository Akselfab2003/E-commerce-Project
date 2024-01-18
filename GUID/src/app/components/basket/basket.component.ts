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
  constructor(private basketTest:basketLogic<T>)
  {

  }
  
  GetBasket() {
    this.basketTest.GetBasket().subscribe(res => this.basket = res)
  };

  ChangeState() {
    this.BasketStateBool = !this.BasketStateBool
    this.BasketState = this.BasketStateBool ? "Open" : "Closed"
    if(this.BasketState == "Open"){
      this.GetBasket();
    }
  }
};
