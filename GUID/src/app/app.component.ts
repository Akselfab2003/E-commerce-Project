import { Component } from '@angular/core';
import { HttpserviceService } from '../Services/httpservice.service';
import { sessionController } from './logic/sessionLogic';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent<T> {
  title = 'GUID';
  constructor(private service:HttpserviceService<T>){
    this.CheckifSessionShouldBeCreated()
  }

  CheckifSessionShouldBeCreated(){
    console.log("Test If This Runs As The First method")
    var CurrentCookieValue:any = sessionController.GetCookie()
    if (CurrentCookieValue==undefined || CurrentCookieValue == null ){
        sessionController.CreateEmptySession(this.service)
    }
    return true;
  }


}
