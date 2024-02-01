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
    console.log(sessionController.GetCookie())
    var sessid = sessionController.GetCookie();
    this.service.GetRequest<Products[]>(`Products/Search?SearchInput=${Input}&sessid=${sessid}`).subscribe((data) => {
      console.log(data)
      this.CurrentProductsOnPage = data;
      this.CurrentProductsDisplayedOnPage = data;
    });
  };

  ngOnInit(): void {
    this.route.paramMap.subscribe(param => {
    var SearchInput:string = param.get("q")?.toString() == undefined ?  "" :  String(param.get("q"));
    console.log(SearchInput)
    this.GetProducts(SearchInput);
    })
  };


  ButtonEvent(event: Event) {
    event.stopPropagation()
    console.log("test")
    //this.GetProducts();
  }

  TagsChangeEventHandler($event: Categories) {
    console.log("Select statment")
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
