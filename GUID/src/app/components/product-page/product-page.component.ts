import { Component } from '@angular/core';
import { Tags } from '../../models/Tags';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Products } from '../../models/Products';
import { Categories } from '../../models/Categories';
import { sessionController } from '../../logic/sessionLogic';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrl: './product-page.component.css'
})
export class ProductPageComponent<T> {

  CurrentProductsOnPage:Products[] = new Array<Products>();
  
  CurrentProductsDisplayedOnPage:Products[] = new Array<Products>();

  constructor(private service:HttpserviceService<T>) { };

  GetProducts<T>():void{
    this.service.GetRequest<Products[]>(`Products/GetLimitedAmountOfProducts/${sessionController.GetCookie()}`).subscribe((data)=>{
      this.CurrentProductsOnPage = data;
      this.CurrentProductsDisplayedOnPage = data;
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

  TagsChangeEventHandler($event:Categories){
    console.log("Select statment")
    if($event.id != 0){
      // this.service.GetRequest<Products[]>("Products/GetProductsThatArePartOfCategory?id="+$event.id).subscribe((data)=>{
      //   this.Product = data;
      // });
      var productsIds:Number[] = this.CurrentProductsOnPage.map(ele => ele.id)  
      this.service.PostRequest<Products[]>("Products/GetProductsThatArePartOfCategory?CategoryId="+$event.id,productsIds).subscribe((data)=>{
        this.CurrentProductsDisplayedOnPage = data;
      });
    }

    this.CurrentProductsDisplayedOnPage = this.CurrentProductsOnPage
  
  }
  
}
