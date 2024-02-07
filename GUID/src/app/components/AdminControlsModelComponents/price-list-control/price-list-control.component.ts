import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Pricelist } from '../../../models/PriceList';
import { Products } from '../../../models/Products';
import { Company } from '../../../models/Company';
import { User } from '../../../models/User';
import { PriceListEntity } from '../../../models/PriceListEntity';
import { JsonPipe } from '@angular/common';
@Component({
  selector: 'app-price-list-control',
  templateUrl: './price-list-control.component.html',
  styleUrl: './price-list-control.component.css'
})
export class PriceListControlComponent<T> {
  // Opretter FormGroup-objekter for skemaerne
  createForm = new FormGroup({
  nameCreate: new FormControl<string>('',Validators.required),
});
  updatePriceListForm= new FormGroup({
    pricelistList:new FormControl<number>(1,Validators.required),
    nameUpdate: new FormControl<string>("",Validators.required),
  });
  AddDeleteItems= new FormGroup({
    priceLists: new FormControl<number>(1,Validators.required),
    productList: new FormControl(),
    priceUpdate: new FormControl<number>(1,Validators.required),
    companyList: new FormControl(),
    usersList: new FormControl(),
  })
  SelectOptions= new FormGroup({
    option: new FormControl<boolean|undefined>(undefined,Validators.required),
  })
  deleteForm = new FormGroup({
    priceListDelete: new FormControl<number>(1,Validators.required)
  })
  selectForm = new FormGroup({
    selectPriceList: new FormControl<number>(1,Validators.required),
    selectDescription: new FormControl(),
  })

   // Variabler for at gemme produkter, virksomheder, brugere og prisliste
  public products:Products[]=[];
  public companies:Company[]=[];
  public users:User[]=[];
  public pricelists:Pricelist[]=[];
  public json:string="";

constructor(private service:HttpserviceService<T>, private router:Router) {};


//Kaldes når brugeren vælger en prisliste
ngOnInit(){
  this.ResetForms();
  this.selectForm.controls["selectPriceList"].valueChanges.subscribe(value =>{
    this.service.GetRequest<Pricelist>("PriceList/PriceList/"+this.selectForm.get("selectPriceList")?.value).subscribe(data=> {
      this.json = JSON.stringify(data);
    });
  });
}

//opretter en prisliste
create(){
  let pricelist:Pricelist= this.InputDataCreate();
  this.service.PostRequest<Pricelist>("PriceList",pricelist).subscribe()
}

//Updaterer en prisliste
updatePriceList(){
  let pricelist:Pricelist= new Pricelist();
  pricelist.id=this.updatePriceListForm.get("pricelistList")?.value as number;
  pricelist.name=this.updatePriceListForm.get("nameUpdate")?.value as string;
  this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList/"+pricelist.id,pricelist).subscribe();
  this.ResetForms();
}

addAndDeleteItems(){
   
  if(this.SelectOptions.get("option")?.value &&this.AddDeleteItems.get("priceLists")?.value!=null){
     
    this.addProduct();
    this.addCompany();
    this.addUser();
  }else if(!this.SelectOptions.get("option")?.value && this.AddDeleteItems.get("priceLists")?.value!=null){
    this.deleteCompany();
    this.deleteProduct();
    this.deleteUser();
  }
  this.ResetForms();
}

delete(){
  let id:number=this.deleteForm.get('priceListDelete')?.value as number;
  this.service.DeleteRequest<Boolean>("PriceList/"+id).subscribe();
  this.ResetForms();
}

//tilføjer et produkt til en prisliste
addProduct(){
  let pricelist:Pricelist = this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) == undefined ? new Pricelist() : this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) as Pricelist;
  if(pricelist.priceListProducts==null){
     
    pricelist.priceListProducts=[];
  }
    let priceEntity:PriceListEntity=new PriceListEntity();
  
    let product:Products=this.AddDeleteItems.get("productList")?.value;
    if(product!=null){
      priceEntity.priceListPrice=this.AddDeleteItems.get("priceUpdate")?.value as number;
      priceEntity.product=product;
      console.log(priceEntity);
      pricelist.priceListProducts.push(priceEntity);
      this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList",pricelist).subscribe();
  }
}


//fjerner et produkt fra en prisliste
deleteProduct(){
  let pricelist:Pricelist = this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) == undefined ? new Pricelist() : this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) as Pricelist;
  if(pricelist.priceListProducts==null){
    pricelist.priceListProducts=[];
  }
    let priceEntity:PriceListEntity=new PriceListEntity();
    let priceEntityList:PriceListEntity[]=[];
  
    let product:Products=this.AddDeleteItems.get("productList")?.value;
    if(product!=null){
      priceEntity.priceListPrice=this.AddDeleteItems.get("priceUpdate")?.value as number;
      priceEntity.product=product;
    
      for(let item of pricelist.priceListProducts){
        if(item!=priceEntity){
          priceEntityList.push(item);
        }
      }
      pricelist.priceListProducts=priceEntityList;
      
      this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList/"+pricelist.id,pricelist).subscribe();
  }
}


//tilføjer en virksomhed til en prisliste
addCompany(){
  let pricelist:Pricelist = this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) == undefined ? new Pricelist() : this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) as Pricelist;
  if(pricelist.companies==null){
    pricelist.companies=[];
  }
    let company:Company=this.AddDeleteItems.get("companyList")?.value;
    if(company!=null){
      pricelist.companies.push(company);
      this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList/"+pricelist.id,pricelist).subscribe();
    }
}


//fjerner en virksomhed fra en prisliste
deleteCompany(){
  let pricelist:Pricelist = this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) == undefined ? new Pricelist() : this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) as Pricelist;
  if(pricelist.companies==null){
    pricelist.companies=[];
  }
    let company:Company=this.AddDeleteItems.get("companyList")?.value;
    if(company!=null){
      let companyList:Company[]=[];
  
      for(let item of pricelist.companies){
        if(item!=company){
          companyList.push(item);
        }
      }
      pricelist.companies=companyList;
      
      this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList/"+pricelist.id,pricelist).subscribe();
  }
}
  
  
  //tilføjer en bruger til en prisliste
  addUser(){
    let pricelist:Pricelist = this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) == undefined ? new Pricelist() : this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) as Pricelist;
    if(pricelist.users==null){
      pricelist.users=[];
    }
    
      let users:User=this.AddDeleteItems.get("usersList")?.value;
      if(users!=null){
        pricelist.users.push(users);
        this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList/"+pricelist.id,pricelist).subscribe();
    }
  }


//fjerner en bruger fra en prisliste
deleteUser(){
  let pricelist:Pricelist = this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) == undefined ? new Pricelist() : this.pricelists.find(ele => ele.id == this.AddDeleteItems.get("priceLists")?.value) as Pricelist;
  if(pricelist.users==null){
    pricelist.users=[];
  }
    let user:User=this.AddDeleteItems.get("usersList")?.value;
    if(user!=null){
      let userList:User[]=[];
  
      for(let item of pricelist.users){
        if(item!=user){
          userList.push(item);
        }
      }
      pricelist.users=userList;
      
      this.service.PutRequest<Pricelist>("PriceList/UpdatePriceList/"+pricelist.id,pricelist).subscribe();
  }

}


//opretter prisliste baseret på brugers input
InputDataCreate():Pricelist{
  let pricelist:Pricelist=new Pricelist();
  pricelist.name=this.createForm.get('nameCreate')?.value as string;
  return pricelist;
}
ResetForms(){
  this.AddDeleteItems.reset();
  this.deleteForm.reset();
  this.updatePriceListForm.reset();
  this.service.GetRequest<Pricelist[]>("PriceList/GetListOfPriceList").subscribe(data=> {
    this.pricelists = data;
  });
  this.AddDeleteItems.controls["priceLists"].valueChanges.subscribe(value =>{
    let isDelete:boolean=this.SelectOptions.controls["option"]?.value as boolean;
      if(isDelete==false){
        this.service.GetRequest<Products[]>("PriceList/Product/"+this.AddDeleteItems.get('priceLists')?.value).subscribe(data=>{
          this.products=data;
        })
        this.service.GetRequest<User[]>("PriceList/Users/"+this.AddDeleteItems.get('priceLists')?.value).subscribe(data=>{
          this.users=data;
        })
        this.service.GetRequest<Company[]>("PriceList/Companies/"+this.AddDeleteItems.get('priceLists')?.value).subscribe(data=>{
          this.companies=data;
        })
      }
    });
  this.SelectOptions.controls["option"].valueChanges.subscribe( value =>
    {
      if(value==true){
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
  });
};
}
