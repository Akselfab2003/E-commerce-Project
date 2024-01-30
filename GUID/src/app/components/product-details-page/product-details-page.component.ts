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
  GetProduct<T>(id:Number){
    this.service.GetRequest<Products>(`Products/${id}`).subscribe((data)=>{
      this.product = data;
      console.log(data)
    });
  };

  GetProductVariants<T>(id:Number){
    this.service.GetRequest<ProductVariants[]>(`ProductVariants/GetProductVariants/${id}`).subscribe((data)=>{
      this.variants = data;
      console.log(data)
    });
  };

  SelectOnChange(){
    console.log(this.SelectedVariant)
  }

  AddToBasket(event: MouseEvent){
    event.stopPropagation()
    if(this.variants.length > 0 ){
      var NewBasketProduct:Products = this.product;
      var test:ProductVariants[] = new Array<ProductVariants>();
      test.push(this.SelectedVariant)
      NewBasketProduct.productVariants = test;
      this.basketTest.AddToBasket(this.product)
    }
    else{
      this.basketTest.AddToBasket(this.product)
    }
  }

  selectedId: number = 0;
  ngOnInit() {
    
    this.route.paramMap.subscribe((data)=>{
      this.selectedId = Number(data.get('id'));
      this.GetProduct(this.selectedId);
      this.GetProductVariants(this.selectedId);

      console.log("ProductDetails Object:");
    })
  }
}
