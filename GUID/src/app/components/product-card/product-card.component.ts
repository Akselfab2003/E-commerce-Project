import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { Products } from '../../models/Products';
import { Basket } from '../../models/Basket';
import { BasketComponent } from '../basket/basket.component';
import { basketLogic } from '../../logic/basketLogic';
import { ProductPageComponent } from '../product-page/product-page.component';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css',
})
export class ProductCardComponent<T> {

  constructor(private basketTest:basketLogic<T>, private productPage:ProductPageComponent<T>)
  {

  }

  @Input() product: Products = new Products();
  
  @ViewChild(BasketComponent<any>) basketComponent!: BasketComponent<any>;

  basket:Basket = new Basket();
  
  AddToBasket(event: MouseEvent){
    event.stopPropagation()
    this.basketTest.AddToBasket(this.product);
  }

  AddProductQuantity(product:Products){
    product.Quantity += 1
  }

  SubtractProductQuantity(product:Products){
    if(product.Quantity -1 <= 0){

    }
    else{
      product.Quantity -= 1
    }
  }

}
