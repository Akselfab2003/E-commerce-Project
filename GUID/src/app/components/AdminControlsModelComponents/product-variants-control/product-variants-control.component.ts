import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { LoginObject } from '../../../models/LoginObject';
import { sessionController } from '../../../logic/sessionLogic';
import { Session } from '../../../models/Session';
import { ProductVariants } from '../../../models/ProductVariants';
import { Products } from '../../../models/Products';


@Component({
  selector: 'app-product-variants-control',
  templateUrl: './product-variants-control.component.html',
  styleUrl: './product-variants-control.component.css'
})
export class ProductVariantsControlComponent<T> {

  @Output() TagsChangedEvent = new EventEmitter<Products>()

  allProducts: Products[] = new Array<Products>();
  CurrentSelectedValue:string = "All";

   //Gemmer brugerens input i variabler
  createForm = new FormGroup({
    productNameCreate: new FormControl<string>('', Validators.required),
    productDescriptionCreate: new FormControl<string>('', Validators.required),
    productPriceCreate: new FormControl<string>('', Validators.required),
    variantValueCreate: new FormControl<string>('', Validators.required),
    productIDCreate: new FormControl<number>(1, Validators.required),
  });
  updateForm = new FormGroup({
    productNameUpdate: new FormControl<string>('', Validators.required),
    productDescriptionUpdate: new FormControl<string>('', Validators.required),
    productPriceUpdate: new FormControl<string>('', Validators.required),
    variantValueUpdate: new FormControl<string>('', Validators.required),
    productIDUpdate: new FormControl<number>(1, Validators.required),
  })
  deleteForm = new FormGroup({
    productnameDelete: new FormControl(''),
  })
  constructor(private service:HttpserviceService<T>, private router:Router) {
  };

  //starter forfra hvis login ikke passer
  register() {
    let productVariant:ProductVariants= this.InputDataCreate();
    this.service.PostRequest<ProductVariants>("ProductVariants?ID=" + (this.createForm.get('productIDCreate')?.value as unknown as number),productVariant).subscribe((data)=>
    console.log(data)
    )
  }
  update(){
    let productVariant:ProductVariants= this.InputDataUpdate();
    this.service.PutRequest<ProductVariants>("ProductVariants/1",productVariant).subscribe((data)=>
    console.log(data)
    );
  }
  delete(){
    let username:string = this.deleteForm.get('productnameDelete')?.value?.toString() as string;
    this.service.DeleteRequest<ProductVariants>("/ProductVariants/1"+username).subscribe((data)=>
    console.log(data)
    );
  }
  InputDataCreate():ProductVariants{
    let productVariant:ProductVariants=new ProductVariants();
    productVariant.parentProduct = this.allProducts[this.createForm.get('productIDCreate')?.value as number];
    productVariant.name=this.createForm.get('productNameCreate')?.value as string;
    productVariant.description=this.createForm.get('productDescriptionCreate')?.value as string;
    productVariant.price= parseInt(this.createForm.get('productPriceCreate')?.value as string);
    productVariant.variantValue=this.createForm.get('variantValueCreate')?.value as string;
    console.log(productVariant)
    return productVariant;
  }
  InputDataUpdate():ProductVariants{
    let productVariant:ProductVariants=new ProductVariants();
    productVariant.parentProduct = this.allProducts[this.createForm.get('productIDCreate')?.value as number];
    productVariant.name=this.createForm.get('productNameCreate')?.value as string;
    productVariant.description=this.createForm.get('productDescriptionCreate')?.value as string;
    productVariant.price= parseInt(this.createForm.get('productPriceCreate')?.value as unknown as string);
    productVariant.variantValue=this.createForm.get('variantValueCreate')?.value as string;
    return productVariant;
  }

  GetAllProducts<T>(){
    this.service.GetRequest<Products[]>("Products/GetAllProducts").subscribe( ele => {
      let Allproduct:Products =  new Products;
      
      Allproduct.id = 0;
      Allproduct.title = "All";
      Allproduct.Active = true;

      this.setpost([...ele,Allproduct]);

    });
  }

  ngOnInit(){
   this.GetAllProducts<Products[]>()
  }

  setpost(ArrayOfCategories:Products[]){
    this.allProducts = ArrayOfCategories.sort((a:Products,b:Products) => a.id-b.id)
    console.log(this.allProducts)

  }

  selectChanged()
  {
 
    this.allProducts.forEach(ele => {
      
      if(ele.title == this.CurrentSelectedValue){
        this.TagsChangedEvent.emit(ele)
        ele.Active = true;
      }
    })
  }
}
