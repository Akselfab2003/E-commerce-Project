import { Component } from '@angular/core';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrl: './product-page.component.css'
})
export class ProductPageComponent {

  

  ButtonEvent(event:Event){
    event.stopPropagation()
    console.log("test")
  }

}
