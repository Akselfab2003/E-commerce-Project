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

  GetProducts<T>():void{
    this.service.GetRequest<Products[]>("Products/GetLimitedAmountOfProducts").subscribe((data)=>{
      this.Product = data;
    });
  };
  
  ngOnInit(): void {
    this.GetProducts();
  };


  ButtonEvent(event:Event){
    event.stopPropagation()
    console.log("test")
    this.GetProducts();
  }

}
