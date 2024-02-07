import { Component, ViewChild } from '@angular/core';
import { HttpserviceService } from '../Services/httpservice.service';
import { sessionController } from './logic/sessionLogic';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent<T> {

  public theme:string = "light";


ChangeTheme($event: boolean) {
   
  if($event){
    this.theme = "dark"
  }
  else{
   this.theme = "light"
  }
}


  title = 'GUID';
  constructor(private service:HttpserviceService<T>){
    this.CheckifSessionShouldBeCreated()
  }

  CheckifSessionShouldBeCreated(){
     
    var CurrentCookieValue:any = sessionController.GetCookie()
    if (CurrentCookieValue==undefined || CurrentCookieValue == null ){
        sessionController.CreateEmptySession(this.service)
    }
    return true;
  }


}
