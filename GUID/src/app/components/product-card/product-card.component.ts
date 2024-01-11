import { Component, Input } from '@angular/core';
import { Products } from '../../models/Products';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent {

  @Input() Test: Products = new Products();

  ButtonEvent(event: Event) {
    event.stopPropagation()
    console.log("test")
    console.log(this.Test)
  }

}
