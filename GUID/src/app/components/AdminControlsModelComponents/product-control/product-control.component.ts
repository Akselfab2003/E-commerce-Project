import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Component, NgModule } from '@angular/core';
import { Products } from '../../../models/Products';
import { Categories } from '../../../models/Categories';
import { ProductVariants } from '../../../models/ProductVariants';
@Component({
  selector: 'app-product-control',
  templateUrl: './product-control.component.html',
  styleUrl: './product-control.component.css'
})
export class ProductControlComponent<T> {
  createForm = new FormGroup({
    titleCreate: new FormControl<string>('',Validators.required),
    descriptionCreate: new FormControl<string>('',Validators.required),
    priceCreate: new FormControl<number>(1,Validators.required),
    categoriesCreate: new FormControl<string>("",Validators.required),
    variantsCreate: new FormControl<string>("",Validators.required)
  });
    updateForm = new FormGroup({
    productList: new FormControl<number>(1,Validators.required),
    titleUpdate: new FormControl<string>('',Validators.required),
    descriptionUpdate: new FormControl<string>('',Validators.required),
    priceUpdate: new FormControl<number>(1,Validators.required),
    categoriesUpdate: new FormControl<string>("",Validators.required),
    variantsUpdate: new FormControl<string>("",Validators.required)
  })
  deleteForm = new FormGroup({
    idDelete: new FormControl<number>(1,Validators.required),
  })
  public tags:Categories[] = [];
  public varaints:ProductVariants[] = [];
  public products:Products[] = [];

  constructor(private service:HttpserviceService<T>, private router:Router) {
  };
  ngOnInit(){
    this.service.GetRequest<Categories[]>("Filter/Categories").subscribe((data)=>{
      for(let item of data){
        this.tags.push(item);
      }
    });
    this.service.GetRequest<ProductVariants[]>("ProductVariants").subscribe((data)=>{
      for(let item of data){
        this.varaints.push(item);
      }
    });
    this.service.GetRequest<Products[]>("Products/GetAllProducts").subscribe((data)=>{
      for(let item of data){
        this.products.push(item);
      }
    });
  }
  create() {
    let product:Products= this.InputDataCreate();
    this.service.PostRequest<Products>("Products",product).subscribe((data)=>
    console.log(data)
    )
  }
  update(){
    let product:Products= this.InputDataUpdate();
    this.service.PutRequest<Products>("Products/"+product.id,product).subscribe((data)=>
    console.log(data)
    );
  }
  delete(){
    let id:number = this.deleteForm.get('idDelete')?.value as number;
    this.service.DeleteRequest<Boolean>("Products/"+id).subscribe((data)=>
    console.log(data)
    );
  }
  InputDataCreate():Products{
    let product:Products=new Products();
    product.title=this.createForm.get('titleCreate')?.value as string;
    product.description=this.createForm.get('descriptionCreate')?.value as string;
    product.price=this.createForm.get('priceCreate')?.value as number;
    for(let tag of this.tags){
      if(tag.name == this.createForm.get('categoriesCreate')?.value as string){
        product.productCategories=tag;
      }
    }
    for(let varaint of this.varaints){
      if(varaint.name == this.createForm.get('variantsCreate')?.value as string){}
        product.productVariants.push(varaint)
    }
    return product;
  }
  InputDataUpdate():Products{
    let product:Products=new Products();
    product.id=this.updateForm.get('productList')?.value as number;
    product.title=this.updateForm.get('titleUpdate')?.value as string;
    product.description=this.updateForm.get('descriptionUpdate')?.value as string;
    product.price=this.updateForm.get('priceUpdate')?.value as number;
    for(let tag of this.tags){
      if(tag.name == this.updateForm.get('categoriesUpdate')?.value as string){
        product.productCategories=tag;
      }
    }
    for(let varaint of this.varaints){
      if(varaint.name == this.updateForm.get('variantsUpdate')?.value as string){}
        product.productVariants.push(varaint)
    }
    return product;
  }
}
