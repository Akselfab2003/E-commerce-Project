import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Pricelist } from '../../../models/PriceList';
import { Products } from '../../../models/Products';
import { Company } from '../../../models/Company';
import { User } from '../../../models/User';
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
    productList: new FormControl<number>(1,Validators.required),
    companyList: new FormControl<string>('',Validators.required),
    usersList: new FormControl<string>('',Validators.required),
  });
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

}

InputDataCreate():Pricelist{
  let pricelist:Pricelist=new Pricelist();
  pricelist.name=this.createForm.get('nameCreate')?.value as string;
  return pricelist;
}

}
