import { Component } from '@angular/core';
import { Tags } from '../../models/Tags';

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

  TagsChangeEventHandler($event:Tags[]){
    var test = $event[0]
    console.log(test)
  }
}
