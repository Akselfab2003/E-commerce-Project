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
    NewBasketProduct.Quantity = 1;
    if(this.variants.length > 0 ){
      var NewBasketProduct:Products = this.product;
   
    
      console.log(this.product)
      var testbasketdetail:BasketDetails = new BasketDetails();
      
      testbasketdetail.variant = this.SelectedVariant;
      testbasketdetail.quantity = this.product.Quantity; 
      console.log(testbasketdetail);
      this.basketTest.AddBasketDetail(testbasketdetail)
    }
    else{
      
      this.basketTest.AddToBasket(NewBasketProduct)
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
