import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Component, NgModule } from '@angular/core';
import { Products } from '../../../models/Products';
import { Categories } from '../../../models/Categories';
import { ProductVariants } from '../../../models/ProductVariants';
import { Images } from '../../../models/Images';
@Component({
  selector: 'app-product-control',
  templateUrl: './product-control.component.html',
  styleUrl: './product-control.component.css'
})
export class ProductControlComponent<T> {
  // Create form for creating a new product
  createForm = new FormGroup({
    titleCreate: new FormControl<string>('',Validators.required),
    descriptionCreate: new FormControl<string>('',Validators.required),
    priceCreate: new FormControl<number>(1,Validators.required),
    categoriesCreate: new FormControl<string>("",Validators.required),
    variantsCreate: new FormControl<string>("",Validators.required),
    imagesCreate: new FormControl(),
  });
  // Create form for updating a product
    updateForm = new FormGroup({
    productList: new FormControl<number>(1,Validators.required),
    titleUpdate: new FormControl<string>('',Validators.required),
    descriptionUpdate: new FormControl<string>('',Validators.required),
    priceUpdate: new FormControl<number>(1,Validators.required),
    categoriesUpdate: new FormControl<string>("",Validators.required),
    variantsUpdate: new FormControl<string>("",Validators.required),
    imagesUpdate: new FormControl(),
  })
  // Create form for deleting a product
  deleteForm = new FormGroup({
    idDelete: new FormControl<number>(1,Validators.required),
  })
  // Create form for selecting a product
  selectForm = new FormGroup({
    productSelect: new FormControl<number>(1, Validators.required),
    titleSelect: new FormControl<string>('',Validators.required),
    descriptionSelect: new FormControl<string>('',Validators.required),
    priceSelect: new FormControl<number>(1,Validators.required),
    categoriesSelect: new FormControl(),
    variantsSelect: new FormControl(),
    imagesSelect: new FormControl(),
  })

// Arrays to hold categories, product variants, products and images
  public tags:Categories[] = [];
  public varaints:ProductVariants[] = [];
  public products:Products[] = [];
  public images:Images[] = [];

  constructor(private service:HttpserviceService<T>, private router:Router) {};


   // Method to initialize the component
  ngOnInit(){
    // Fetch all categories from the server
    this.service.GetRequest<Categories[]>("Filter/Categories").subscribe((data)=>{
      for(let item of data){
        this.tags.push(item);
      }
    });

// Fetch all product variants from the server
    this.service.GetRequest<ProductVariants[]>("ProductVariants").subscribe((data)=>{
      for(let item of data){
        this.varaints.push(item);
      }
    });

// Fetch all products from the server
    this.service.GetRequest<Products[]>("Products/GetAllProducts").subscribe((data)=>{
      for(let item of data){
        this.products.push(item);
      }
    });

// Fetch all images from the server
    this.service.GetRequest<Images[]>("Image/GetAllImages").subscribe((data)=>{
      for(let item of data){
        this.images.push(item);
      }
    });

// Subscribe to changes in the product selection form
    this.selectForm.controls["productSelect"].valueChanges.subscribe(value =>{
      if(value != null){
        this.selectOnChange(value)
      }
    });
  }

// Method to create a new product
  create() {
    let product:Products= this.InputDataCreate();
    this.service.PostRequest<Products>("Products/CreateProduct",product).subscribe()
    this.UpdateProductsList();
  }

// Method to update a product
  update(){
    let product:Products= this.InputDataUpdate();
    this.service.PutRequest<Products>("Products/"+product.id,product).subscribe();
    this.UpdateProductsList();
  }

// Method to delete a product
  delete(){
    let product:Products=this.products.find(ele => ele.id == this.deleteForm.get("idDelete")?.value) == undefined ? new Products() : this.products.find(ele => ele.id == this.deleteForm.get("idDelete")?.value) as Products;
    this.service.DeleteRequest<Boolean>("Products/"+product.id).subscribe();
    this.UpdateProductsList();
  }

//// Method to gather input data for creating a product
  InputDataCreate():Products{
    let product:Products=new Products();
    product.title=this.createForm.get('titleCreate')?.value as string;
    product.description=this.createForm.get('descriptionCreate')?.value as string;
    product.price=this.createForm.get('priceCreate')?.value as number;
    // Loop through each tag in the 'tags' array
    for(let tag of this.tags){
      if(tag.name == this.createForm.get('categoriesCreate')?.value as string){
        product.productCategories=tag;
      }
    }
    // Loop through each variant in the 'varaints' array
    for(let varaint of this.varaints){
      if(varaint.name == this.createForm.get('variantsCreate')?.value as string){
        product.productVariants.push(varaint)
      }
    }
    // Loop through each image in the 'images' array
    for(let image of this.images){
      if(image.id == this.createForm.get('imagesCreate')?.value.id as number){
        product.images.push(image)
      }
    }
    return product;
  }

// Method to gather input data for updating a product
  InputDataUpdate():Products{
    let product:Products=this.products.find(ele => ele.id == this.updateForm.get("productList")?.value) == undefined ? new Products() : this.products.find(ele => ele.id == this.updateForm.get("productList")?.value) as Products;
    product.id=this.updateForm.get('productList')?.value as number;
    product.title=this.updateForm.get('titleUpdate')?.value as string;
    product.description=this.updateForm.get('descriptionUpdate')?.value as string;
    product.price=this.updateForm.get('priceUpdate')?.value as number;
    // Loop through each tag in the 'tags' array
    for(let tag of this.tags){
      if(tag.name == this.updateForm.get('categoriesUpdate')?.value as string){
        product.productCategories=tag;
      }
    }
    // Loop through each variant in the 'varaints' array
    for(let varaint of this.varaints){
      if(varaint.name == this.updateForm.get('variantsUpdate')?.value as string){
        product.productVariants.push(varaint)
      }
    }
    // Loop through each image in the 'images' array
    for(let image of this.images){
      if(image.id == this.updateForm.get('imagesUpdate')?.value.id as number){
        product.images.push(image)
      }
    }
    return product;
  }

// Method to handle changes in the product selection form
    selectOnChange(value:number | null):void{
      // Find the product with the id matching the passed value if no product is found, create a new product
    var product:Products =this.products.find(ele => ele.id == value) == undefined ? new Products() : this.products.find(ele => ele.id == value) as Products;
    if(product != new Products() && product.productCategories!=null){
      // If the product has images
      // Set the values of the 'selectForm' to the properties of the product
      if(product.images.length!=0){
        this.selectForm.setValue({
          titleSelect: product.title,
         descriptionSelect:product.description,
          productSelect: product.id,
          priceSelect: product.price,
          categoriesSelect: product.productCategories.name,
          variantsSelect: product.productVariants,
          imagesSelect: product.images[0].imagePath,
        },{emitEvent:false})
      }else{
        // Set the values of the 'selectForm' to the properties of the product, with 'imagesSelect' set to the images of the product
        this.selectForm.setValue({
          titleSelect: product.title,
         descriptionSelect:product.description,
          productSelect: product.id,
          priceSelect: product.price,
          categoriesSelect: product.productCategories.name,
          variantsSelect: product.productVariants,
          imagesSelect: product.images,
        },{emitEvent:false})
      }
    }else{
      // Set the values of the 'selectForm' to the properties of the product, with 'categoriesSelect' set to the product category of the product
      this.selectForm.setValue({
        titleSelect: product.title,
       descriptionSelect:product.description,
        productSelect: product.id,
        priceSelect: product.price,
        categoriesSelect: product.productCategories,
        variantsSelect: product.productVariants,
        imagesSelect: product.images,
      },{emitEvent:false})
    }
  }

 // Method to update the list of products
  UpdateProductsList():void{
    this.products=[];
    this.service.GetRequest<Products[]>("Products/GetAllProducts").subscribe((data)=>{
      for(let item of data){
        this.products.push(item);
      }
    });
  }
}
