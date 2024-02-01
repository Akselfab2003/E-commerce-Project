import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { Products } from '../../models/Products';
import { Basket } from '../../models/Basket';
import { BasketComponent } from '../basket/basket.component';
import { basketLogic } from '../../logic/basketLogic';
import { ProductPageComponent } from '../product-page/product-page.component';
import { Images } from '../../models/Images';

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
    this.basketTest.AddToBasket(this.product,undefined,1);
  }

  AddProductQuantity(event: MouseEvent){
    event.stopPropagation()
    this.product.Quantity += 1
  }

  SubtractProductQuantity(event: MouseEvent){
    if(this.product.Quantity -1 <= 0){

    }
    else{
      event.stopPropagation()
      this.product.Quantity -= 1
    }
  }




  ImageCheckSrc(images:Images[]){
    
    if(images.length == 0){
      return "https://fastly.picsum.photos/id/1025/1000/1500.jpg?hmac=1LkHmpIJnIt0_dmhCgO3F2PNh8RM2zXK13TcTtKK-1A"
    } 
    else{
      return images[0].imagePath;
    }
    
   // console.log(event.target)
    //this.src='https://fastly.picsum.photos/id/1025/1000/1500.jpg?hmac=1LkHmpIJnIt0_dmhCgO3F2PNh8RM2zXK13TcTtKK-1A'
  }

}
