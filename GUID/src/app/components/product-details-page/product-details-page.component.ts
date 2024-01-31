import { Component, Input } from '@angular/core';
import { Products } from '../../models/Products';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { basketLogic } from '../../logic/basketLogic';
import { ProductVariants } from '../../models/ProductVariants';
import { FormControl, Validators } from '@angular/forms';
import { LoginObject } from '../../models/LoginObject';
import { BasketDetails } from '../../models/BasketDetails';

@Component({
  selector: 'app-product-details-page',
  templateUrl: './product-details-page.component.html',
  styleUrl: './product-details-page.component.css'
})
export class ProductDetailsPageComponent<T> {
  constructor(private route: ActivatedRoute, private service: HttpserviceService<T>, private basketTest:basketLogic<T>) {}
  public variants:ProductVariants[] = new Array<ProductVariants>();
  @Input() product: Products = new Products();

  public SelectedVariant:ProductVariants = new ProductVariants();

  public SelectedIdFromVariant:number = 0;
  GetProduct<T>(id:Number){
    this.service.GetRequest<Products>(`Products/${id}`).subscribe((data)=>{
      this.product = data;
      this.product.Quantity = 1;
      console.log("GET PRODUCT TEST", data)
    });
  };

  GetProductVariants<T>(id:Number){
    this.service.GetRequest<ProductVariants[]>(`ProductVariants/GetProductVariants/${id}`).subscribe((data)=>{
      this.variants = data;
      console.log(data)
    });
  };

  SelectOnChange(){

    var test:ProductVariants = new ProductVariants()
    if(this.variants.length > 0){
      var testtest
       = this.variants.find(variant => variant.id == this.SelectedIdFromVariant)  ==  undefined ? new ProductVariants() :  this.variants.find(variant => variant.id == this.SelectedIdFromVariant);
      this.SelectedVariant= testtest == undefined ? new ProductVariants(): testtest;
    }
   
    console.log(this.SelectedVariant)
  }

  AddToBasket(event: MouseEvent){
    event.stopPropagation()
    var NewBasketProduct:Products = this.product;
    console.log((Object.entries(this.SelectedVariant).toString() != Object.entries(new ProductVariants()).toString()) )
    
    console.log((Object.entries(this.product).toString() != Object.entries(new Products()).toString()) )
    this.basketTest.AddToBasket(((Object.entries(this.SelectedVariant).toString() != Object.entries(new ProductVariants()).toString()) ? undefined : this.product),(  (this.variants.length == 0) ? undefined : this.SelectedVariant),(this.product == undefined ? 1:undefined))

  }

  AddProductQuantity(event: MouseEvent){
    event.stopPropagation()
    this.product.Quantity += 1
    console.log(this.product.Quantity)
    
  }

  SubtractProductQuantity(event: MouseEvent){
    if(this.product.Quantity -1 <= 0){

    }
    else{
      event.stopPropagation()
      this.product.Quantity -= 1
      console.log(this.product.Quantity)
    }
  }

  ngOnInit() {
    var selectedId:Number = 0;
    this.route.paramMap.subscribe((data)=>{
      selectedId = Number(data.get('id'));
      
      this.GetProduct(selectedId);
      this.GetProductVariants(selectedId);

      console.log("ProductDetails Object:");
    })
  }
}
