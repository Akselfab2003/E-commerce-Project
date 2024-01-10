import { HttpClient, HttpHeaders } from "@angular/common/http";
import { RequestType } from "./request-type";
import { Observable, catchError } from "rxjs";
import { HttpModule } from "./HttpModule";



export class HttpserviceService{

    // private http:HttpClient;
    // private url:string;
    // private type:RequestType;
    // private dataObject:Object;

    // constructor(Http:HttpClient,url:string,type:RequestType,dataObject:Object){
    //     this.http = Http;
    //     this.url = url;
    //     this.type = type;
    //     this.dataObject = dataObject;
    // }
    private http:HttpClient;
    private HttpRequest:HttpModule;
  
    constructor(Http:HttpClient,HttpRequest:HttpModule){
        this.http = Http
        this.HttpRequest=HttpRequest
    }

   public SendHttpRequest(){
        switch(this.HttpRequest.type){
            case RequestType.Get:
              return  this.GetRequest();
                break;
            case RequestType.Post:
                return   this.PostRequest();
                break;
            case RequestType.Put:
                return  this.PutRequest();
                break;
            case RequestType.DELETE:
                return  this.DeleteRequest();
                break;
        }        
    }


   private GetRequest() : Observable<any>{
      
        var terst =  this.http.get(this.HttpRequest.url );

        return terst;
    }
    private PostRequest() :Observable<any>
    {
        return this.http.post<any>(this.HttpRequest.url,this.HttpRequest.dataObject).pipe( 
        );
    }

    private  PutRequest() :Observable<any>
    {
        return this.http.put<any>(this.HttpRequest.url,this.HttpRequest.dataObject).pipe( 
        );  
    }
    private  DeleteRequest() :Observable<any>
    {
        return this.http.delete<any>(this.HttpRequest.url).pipe( 
        );  
    }
}





  /* CreateHttpRequest(Http:HttpClient,url:string,type:RequestType,dataObject:Object){
        switch(this.type){
            case RequestType.Get:
                this.GetRequest();
                break;
            case RequestType.Post:
                this.PostRequest();
                break;
            case RequestType.Put:
                this.PutRequest();
                break;
            case RequestType.DELETE:
                this.DeleteRequest();
                break;
        }        
    }


    GetRequest(){
        return this.http.get<Object>(this.url);
    }
    PostRequest() :Observable<object>
    {
        return this.http.post<Object>(this.url,this.dataObject).pipe( 
        );
    }

    PutRequest() :Observable<object>
    {
        return this.http.put<Object>(this.url,this.dataObject).pipe( 
        );  
    }
    DeleteRequest() :Observable<object>
    {
        return this.http.delete<Object>(this.url).pipe( 
        );  
    } */