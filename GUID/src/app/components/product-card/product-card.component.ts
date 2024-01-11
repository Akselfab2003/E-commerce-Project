import { Component, Input } from '@angular/core';
import { Products } from '../../models/Products';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent {

  @Input() product: Products = new Products();

  ButtonEvent(event: Event) {
    event.stopPropagation()
    console.log("product")
    console.log(this.product)
  }

}
