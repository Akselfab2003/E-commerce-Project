import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';
@Injectable({
  providedIn: 'root'

})
export class HttpserviceService<T> {

  private API_URL: string = ""


  constructor(private Http: HttpClient) {
    this.API_URL = `${environment.API_URL}`
  }

  GetRequest<T>(ENDPOINT: string): Observable<T> {
    var Full_URL = this.API_URL + ENDPOINT

    return this.Http.get<T>(Full_URL, { withCredentials: true });
  }

  PostRequest<T>(ENDPOINT: string, DATAOBJECT: any): Observable<T> {
    var Full_URL = this.API_URL + ENDPOINT
    return this.Http.post<T>(Full_URL, DATAOBJECT, { withCredentials: true });
  }

  PutRequest<T>(ENDPOINT: string, DATAOBJECT: any): Observable<T> {
    var Full_URL = this.API_URL + ENDPOINT
    return this.Http.put<T>(Full_URL, DATAOBJECT, { withCredentials: true });
  }

  DeleteRequest<T>(ENDPOINT: string): Observable<T> {
    var Full_URL = this.API_URL + ENDPOINT
    return this.Http.delete<T>(Full_URL, { withCredentials: true });
  }

}
