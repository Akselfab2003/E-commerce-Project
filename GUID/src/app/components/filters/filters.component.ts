import { Component, EventEmitter, Output } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Tags } from '../../models/Tags';


@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent <T> {

  @Output() TagsChangedEvent = new EventEmitter<Tags[]>()
  
  TagsList:Tags[] = new Array<Tags>();

  constructor(private service:HttpserviceService<T>){
  } 

  GetAllTags<T>(){
    this.service.GetRequest<Tags[]>("Tags/Categories").subscribe( ele => {
      this.setpost(ele)
   });

   
  }

  ngOnInit(){
   this.GetAllTags<Tags[]>()
  }

  setpost(ArrayOfTags:Tags[]){
    this.TagsList = ArrayOfTags
    console.log(this.TagsList)
    this.TagsChangedEvent.emit(this.TagsList)
  }


}
