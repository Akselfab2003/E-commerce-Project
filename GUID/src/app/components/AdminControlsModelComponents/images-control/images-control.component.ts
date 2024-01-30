import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule  } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { Products } from '../../../models/Products';
import { Images } from '../../../models/Images';
import { Observable, empty } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-images-control',
  templateUrl: './images-control.component.html',
  styleUrl: './images-control.component.css'
})
export class ImagesControlComponent<T> implements OnInit {
  createForm = new FormGroup({
    urlCreate: new FormControl<string>('',Validators.required),
  });
    updateForm = new FormGroup({
    imageUpdate: new FormControl<string>("",Validators.required),
    urlUpdate:new FormControl<string>("",Validators.required),
  })
  deleteForm = new FormGroup({
    imageDelete: new FormControl(),
  })
  selectForm = new FormGroup({
    imageSelect: new FormControl(),
  })
  public products:Products[] = [];
  public images:Images[] = [];
  constructor(private service:HttpserviceService<T>, private router:Router) {
  };
  ngOnInit(){
    this.service.GetRequest<Products[]>("Products/GetAllProducts").subscribe((data)=>{
      for(let item of data){
        this.products.push(item);
      }
    });
    this.UpdateImagesList();
  }
  create() {
    let image:Images= this.InputDataCreate();
    this.service.PostRequest<Images>("Image",image).subscribe()
    this.UpdateImagesList();
  }
  update(){
    let image:Images=this.inputDataUpdate();
    this.service.PutRequest<Images>("Image/"+image.id,image).subscribe();
    this.UpdateImagesList();
  }
  delete(){
    let id:number=this.deleteForm.get('imageDelete')?.value.id as number;
    this.service.DeleteRequest<Boolean>("Image/"+id).subscribe();
    this.UpdateImagesList();
  }
  InputDataCreate():Images{
    let image:Images=new Images();
    image.imagePath=this.createForm.get('urlCreate')?.value as string;
    return image;
  }
  inputDataUpdate():Images{
    let image:Images=this.images.find(ele => ele.imagePath == this.updateForm.get("imageUpdate")?.value) == undefined ? new Images() : this.images.find(ele => ele.imagePath == this.updateForm.get("imageUpdate")?.value) as Images;
    image.imagePath=this.updateForm.get('urlUpdate')?.value as string;
    return image;
  }
  UpdateImagesList():void{
    this.images=[];
    this.service.GetRequest<Images[]>("Image/GetAllImages").subscribe((data)=>{
      for(let item of data){
        this.images.push(item);
      }
    });
  }
}
