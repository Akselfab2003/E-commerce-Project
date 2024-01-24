import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Component, NgModule } from '@angular/core';
import { Products } from '../../../models/Products';
import { Categories } from '../../../models/Categories';
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
    idUpdate:new FormControl(1,Validators.required),
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

  constructor(private service:HttpserviceService<T>, private router:Router) {
  };
  ngOnInit(){
    this.service.GetRequest<Categories[]>("Filter/Categories").subscribe((data)=>{
      for(let item of data){
        this.tags.push(item);
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
    /*
    product.productCategories=this.createForm.get('categoriesCreate')?.value?.toString();
    product.productVariants=this.createForm.get('variantsCreate')?.value?.toString();
    */
    return product;
  }
  InputDataUpdate():Products{
    let product:Products=new Products();
    product.id=this.updateForm.get('idUpdate')?.value as number;
    product.title=this.updateForm.get('titleUpdate')?.value as string;
    product.description=this.updateForm.get('descriptionUpdate')?.value as string;
    product.price=this.updateForm.get('priceUpdate')?.value as number;
    /*
    product.productCategories=this.createForm.get('categoriesCreate')?.value?.toString();
    product.productVariants=this.createForm.get('variantsCreate')?.value?.toString();
    */
    return product;
  }
}
