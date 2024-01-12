import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
  providers:[HttpserviceService]
})



export class HomePageComponent <T> {
 
  constructor(
    private service:HttpserviceService<T>
  ){}

  ngOnInit(){
    this.sendrequest();

  }


  sendrequest(){
  }




}
