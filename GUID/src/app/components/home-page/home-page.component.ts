import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { sessionController } from '../../logic/sessionLogic';
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
    sessionController.GetCookie();
  }


  sendrequest(){
  }




}
