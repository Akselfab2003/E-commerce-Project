import { Component, Input } from '@angular/core';
import { Products } from '../../models/Products';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { sessionController } from '../../logic/sessionLogic';
import { Categories } from '../../models/Categories';
import { ActivatedRoute } from '@angular/router';
import { ProductCardComponent } from '../product-card/product-card.component';
import { ProductPageComponent } from '../product-page/product-page.component';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrl: './search-result.component.css',
  providers:[ProductPageComponent]
})
export class SearchResultComponent<T> {
  constructor(private route:ActivatedRoute,private service: HttpserviceService<T>) { };
  CurrentProductsOnPage: Products[] = new Array<Products>();
  
  CurrentProductsDisplayedOnPage: Products[] = new Array<Products>();

  @Input() SearchInput: string = "";


  GetProducts(Input:string): void {
     
    var sessid = sessionController.GetCookie();
    this.service.GetRequest<Products[]>(`Products/Search?SearchInput=${Input}&sessid=${sessid}`).subscribe((data) => {
      var newProductsArray:Products[] = new Array<Products>();

      newProductsArray = data.filter(ele => ele.title != "").map(ele => ele = {id: ele.id, title: ele.title, description: ele.description, images: ele.images, price: ele.price, productCategories: ele.productCategories, productVariants: ele.productVariants, Quantity: 1, Active: ele.Active} )
       

      this.CurrentProductsOnPage = newProductsArray;
      this.CurrentProductsDisplayedOnPage = newProductsArray;
    });
  };
 

  ngOnInit(): void {
    this.route.paramMap.subscribe(param => {
    var SearchInput:string = param.get("q")?.toString() == undefined ?  "" :  String(param.get("q"));
     
    this.GetProducts(SearchInput);
    })
  };


  ButtonEvent(event: Event) {
    event.stopPropagation()
     
    //this.GetProducts();
  }

  TagsChangeEventHandler($event: Categories) {
     
    if ($event.id != 0) {
      // this.service.GetRequest<Products[]>("Products/GetProductsThatArePartOfCategory?id="+$event.id).subscribe((data)=>{
      //   this.Product = data;
      // });
      var productsIds: Number[] = this.CurrentProductsOnPage.map(ele => ele.id)
      this.service.PostRequest<Products[]>("Products/GetProductsThatArePartOfCategory?CategoryId=" + $event.id +"&sessid="+sessionController.GetCookie(), productsIds).subscribe((data) => {
        this.CurrentProductsDisplayedOnPage = data;
      });
    }

    this.CurrentProductsDisplayedOnPage = this.CurrentProductsOnPage

  }

}
