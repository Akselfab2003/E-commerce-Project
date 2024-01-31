import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Pricelist } from '../../../models/PriceList';
import { Products } from '../../../models/Products';
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
    
  });
  public products:Products[]=[];
constructor(private service:HttpserviceService<T>, private router:Router) {
};

ngOnInit(){
  this.service.GetRequest<Products[]>("Products/GetAllProducts").subscribe((data)=>{
    for(let item of data){
      this.products.push(item);
    }
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
