import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Pricelist } from '../../../models/PriceList';
import { Products } from '../../../models/Products';
import { Company } from '../../../models/Company';
import { User } from '../../../models/User';
import { PriceListEntity } from '../../../models/PriceListEntity';
@Component({
  selector: 'app-price-list-control',
  templateUrl: './price-list-control.component.html',
  styleUrl: './price-list-control.component.css'
})
export class PriceListControlComponent<T> {
  createForm = new FormGroup({
  nameCreate: new FormControl<string>('',Validators.required),
});
  updatePriceListForm= new FormGroup({
    pricelistList:new FormControl<number>(1,Validators.required),
    nameUpdate: new FormControl<string>("",Validators.required),
  });
  AddDeleteItems= new FormGroup({
    priceLists: new FormControl(),
    productList: new FormControl(),
    companyList: new FormControl(),
    usersList: new FormControl(),
  })
  public products:Products[]=[];
  public companies:Company[]=[];
  public users:User[]=[];
  public pricelists:Pricelist[]=[];
constructor(private service:HttpserviceService<T>, private router:Router) {
};

ngOnInit(){
  this.service.GetRequest<Products[]>("Products/GetAllProducts").subscribe((data)=>{
    this.products=data;
    });
  this.service.GetRequest<Company[]>("Company/GetAllCompanies").subscribe(data=> {
    this.companies = data;
  });
  this.service.GetRequest<User[]>("User/GetListOfUsers").subscribe(data=> {
    this.users = data;
  });
  this.service.GetRequest<Pricelist[]>("PriceList/GetListOfPriceList").subscribe(data=> {
    this.pricelists = data;
  });
}

create(){
  let pricelist:Pricelist= this.InputDataCreate();
  this.service.PostRequest<Pricelist>("PriceList",pricelist).subscribe()
}

updatePriceList(){
  let pricelist:Pricelist= new Pricelist();
  pricelist.id=this.updatePriceListForm.get("pricelistList")?.value as number;
  pricelist.name=this.updatePriceListForm.get("nameUpdate")?.value as string;
  this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList",pricelist).subscribe();
}

addProduct(){
  let pricelist:Pricelist = this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value.id) == undefined ? new Pricelist() : this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value.id) as Pricelist;
  if(pricelist.priceListProducts==null){
    pricelist.priceListProducts=[];
  }
  let priceEntity:PriceListEntity=new PriceListEntity();
  
  let product:Products=this.AddDeleteItems.get("productList")?.value;
  priceEntity.priceListPrice=product.price;
  priceEntity.product=product;

  pricelist.priceListProducts.push(priceEntity);
  this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList",pricelist).subscribe();
}

deleteProduct(){
  let pricelist:Pricelist = this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value.id) == undefined ? new Pricelist() : this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value.id) as Pricelist;
  if(pricelist.priceListProducts==null){
    pricelist.priceListProducts=[];
  }
  let priceEntity:PriceListEntity=new PriceListEntity();
  let priceEntityList:PriceListEntity[]=[];

  let product:Products=this.AddDeleteItems.get("productList")?.value;
  priceEntity.priceListPrice=product.price;
  priceEntity.product=product;

  console.log();
  for(let item of pricelist.priceListProducts){
    if(item!=priceEntity){
      priceEntityList.push(item);
    }
  }
  pricelist.priceListProducts=priceEntityList;
  
  this.service.PutRequest<Pricelist>("UpdatePriceList",pricelist).subscribe();
}

addCompany(){

}
deleteCompany(){

}

addUser(){

}
deleteUser(){

}

InputDataCreate():Pricelist{
  let pricelist:Pricelist=new Pricelist();
  pricelist.name=this.createForm.get('nameCreate')?.value as string;
  return pricelist;
}

}
