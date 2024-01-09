import { RequestType } from "./request-type";

export class HttpModule{
    
     constructor(_url:string,_type:RequestType,_dataObject:Object){
          this.url = _url
          this.type = _type
          this.dataObject = _dataObject
          
     }
    public url:string ="";
    public type:RequestType = RequestType.Get;
    public dataObject:Object= Object
}