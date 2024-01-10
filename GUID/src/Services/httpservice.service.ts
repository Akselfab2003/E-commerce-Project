import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestType } from '../app/Functions modules/request-type';
import { HttpModule } from '../app/Functions modules/HttpModule';
import { HttpserviceService  as HttpserviceModule} from '../app/Functions modules/HttpserviceModule';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'

})
export class HttpserviceService {
  private http:HttpClient;
  // private HttpRequest:HttpModule;
  constructor(Http:HttpClient){
      this.http = Http
      // this.HttpRequest=HttpRequest
  }

  CreateHttpRequest(HttpRequest:HttpModule) : Observable<any>{
     var test = new HttpserviceModule(this.http,HttpRequest)
     return test.SendHttpRequest()
  }
  
}