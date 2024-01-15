import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input } from '@angular/core';

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
export class BasketComponent {
  public BasketState:string = "Closed"
  public BasketStateBool:boolean = false

  ChangeState() {
    this.BasketStateBool = !this.BasketStateBool
    this.BasketState = this.BasketStateBool ? "Open" : "Closed"
  }
}
