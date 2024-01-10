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

  public  TagsList:Tags[] = Array()
  constructor(private service:HttpserviceService<T>){
    this.GetAllTags();
  } 

  GetAllTags<T>(){
    this.service.GetRequest<Tags[]>("Tags").subscribe(ele => {
      this.TagsList = ele
    })
    console.log(this.TagsList)
  }


}
