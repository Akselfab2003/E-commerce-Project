import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { environment } from '../../../environments/environment.development';
import { Tags } from '../../models/Tags';

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent <T> {

  private TagsList:Tags[] = new Array<Tags>();
  constructor(private service:HttpserviceService<T>){
    
  } 

  GetAllTags<T>(){
    this.service.GetRequest<Tags[]>("Tags").subscribe( ele => {
    this.TagsList = ele
   });

    // this.service.GetRequest<Tags[]>("Tags").subscribe(ele=>{
    //   console.log(ele)
    //   //console.log(typeof(ele))
    //   this.TagsList = ele
    // });
  }

  ngOnInit(){
   this.GetAllTags<Tags[]>()
  }

  test(){
    console.log( this.TagsList)
  }

}
