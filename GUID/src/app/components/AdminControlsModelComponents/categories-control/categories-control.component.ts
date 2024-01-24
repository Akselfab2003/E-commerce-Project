import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Products } from '../../../models/Products';
import { Categories } from '../../../models/Categories';
import { ProductVariants } from '../../../models/ProductVariants';

@Component({
  selector: 'app-categories-control',
  templateUrl: './categories-control.component.html',
  styleUrl: './categories-control.component.css'
})
export class CategoriesControlComponent<T> {
    createForm = new FormGroup({
      nameCreate: new FormControl<string>('',Validators.required),
    });
      updateForm = new FormGroup({
      categoriesUpdate: new FormControl<string>("",Validators.required),
      nameUpdate:new FormControl<string>("",Validators.required),
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
      let categorie:Categories= this.InputDataCreate();
      this.service.PostRequest<Categories>("Filter/Categories",categorie).subscribe((data)=>
      console.log(data)
      )
      this.service.GetRequest<Categories[]>("Filter/Categories").subscribe((data)=>{
        for(let item of data){
          this.tags.push(item);
        }
      });
    }
    update(){
      let categorie:Categories= this.InputDataUpdate();
      this.service.PutRequest<Categories>("Filter/"+categorie.id,categorie).subscribe((data)=>
      console.log(data)
      );
      this.service.GetRequest<Categories[]>("Filter/Categories").subscribe((data)=>{
        for(let item of data){
          this.tags.push(item);
        }
      });
    }
    delete(){
      let id:number = this.deleteForm.get('idDelete')?.value as number;
      this.service.DeleteRequest<Boolean>("Filter/"+id).subscribe((data)=>
      console.log(data)
      );
      this.service.GetRequest<Categories[]>("Filter/Categories").subscribe((data)=>{
        for(let item of data){
          this.tags.push(item);
        }
      });
    }
    InputDataCreate():Categories{
      let categorie:Categories=new Categories();
      categorie.name=this.createForm.get('nameCreate')?.value as string;
      return categorie;
    }
    InputDataUpdate():Categories{
      let categorie:Categories = new Categories();
      for(let tag of this.tags){
        if(tag.name == this.updateForm.get('categoriesUpdate')?.value as string){
          categorie=tag;
        }
      }
      categorie.name=this.updateForm.get('nameUpdate')?.value as string;
      return categorie;
    }
  }
