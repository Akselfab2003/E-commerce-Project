import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, catchError } from "rxjs";



export class HttpserviceService<T>{

    constructor(private Http:HttpClient,private HttpRequest:HttpModule){
    }

    public SendHttpRequest(){
        switch(this.HttpRequest.type){
            case RequestType.Get:
                return  this.GetRequest();
                break;
            case RequestType.Post:
                return  this.PostRequest();
                break;
            case RequestType.Put:
                return  this.PutRequest();
                break;
            case RequestType.DELETE:
                return  this.DeleteRequest();
                break;
        }        
    }

    private GetRequest() : Observable<T>{ 
        return this.Http.get<T>(this.HttpRequest.url);
    }
    private PostRequest() :Observable<any>
    {
        return this.Http.post<T>(this.HttpRequest.url,this.HttpRequest.dataObject).pipe();
    }

    private  PutRequest() :Observable<T>
    {
        return this.Http.put<T>(this.HttpRequest.url,this.HttpRequest.dataObject).pipe();  
    }
    private  DeleteRequest() :Observable<T>
    {
        return this.Http.delete<T>(this.HttpRequest.url).pipe();  
    }
}
