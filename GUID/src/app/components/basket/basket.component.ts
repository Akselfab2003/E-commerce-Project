import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input } from '@angular/core';
import { Products } from '../../models/Products';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { HttpserviceService } from '../../../Services/httpservice.service';

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

  constructor(private route: ActivatedRoute, private service: HttpserviceService<T>) {}

  @Input() product: Products = new Products();

  /*
  GetProduct<T>(id:Number){
    this.service.GetRequest<BasketDetails>(`Products/${id}`).subscribe((data)=>{
      this.product = data;
      console.log(data)
    });
  };

  selectedId: number = 0;
  ngOnInit() {
      this.route.paramMap.subscribe((data)=>{
      this.selectedId = Number(data.get('id'));
      this.GetProduct(this.selectedId)
      console.log("ProductDetails Object:");
    })
  }
*/
  ChangeState() {
    this.BasketStateBool = !this.BasketStateBool
    this.BasketState = this.BasketStateBool ? "Open" : "Closed"
  }
}
