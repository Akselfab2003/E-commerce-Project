import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Products } from '../../models/Products';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrl: './product-page.component.css'
})
export class ProductPageComponent<T> {

  Product:Products[] = new Array<Products>();

  constructor(private service:HttpserviceService<T>) { };

  ProductImage<T>():void{
    this.service.GetRequest<Products>("Products/1").subscribe((data)=>{
      this.Product = new Array<Products>(data, data, data, data);
    });
  };
  
  ngOnInit(): void {
    this.ProductImage();
  };


  ButtonEvent(event:Event){
    event.stopPropagation()
    console.log("test")
    this.ProductImage();
  }

}
