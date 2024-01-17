import { Component, EventEmitter, Output } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Tags } from '../../models/Tags';
import { Categories } from '../../models/Categories';


@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent <T> {


  @Output() TagsChangedEvent = new EventEmitter<Categories>()
  
  Categories:Categories[] = new Array<Categories>();
  CurrentSelectedValue:string = "All";
  constructor(private service:HttpserviceService<T>){
  }  

  GetAllTags<T>(){
    this.service.GetRequest<Categories[]>("Tags/Categories").subscribe( ele => {
      
      let AllCategory:Categories =  new Categories;
      
      AllCategory.id = 0;
      AllCategory.name = "All";
      AllCategory.Active = true;

      this.setpost([...ele,AllCategory]);

    })
  }

  ngOnInit(){
   this.GetAllTags<Categories[]>()
  }

  setpost(ArrayOfCategories:Categories[]){
    this.Categories = ArrayOfCategories.sort((a:Categories,b:Categories) => a.id-b.id)
    console.log(this.Categories)

  }

  selectChanged()
  {
 
    this.Categories.forEach(ele => {
      
      if(ele.name == this.CurrentSelectedValue){
        this.TagsChangedEvent.emit(ele)
        ele.Active = true;
      }
    })
  }


}
