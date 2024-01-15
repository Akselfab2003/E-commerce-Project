import { AbstractControl } from "@angular/forms";
import { HttpserviceService } from "../../Services/httpservice.service";
import { Session } from "../models/Session"

export class sessionController<T>{
    public static service:HttpserviceService<any>;
    constructor(private Service:HttpserviceService<T>){
       sessionController.service = Service
    }
    public static GetCookie():string{
        var cookieArray=document.cookie.split(";");
        var sessId=cookieArray[0].split("=")[1];
        return sessId;
    }
    public static SetCookie(session:Session):void{
        const COOKIE_NAME:string="sessionId";
        const PATH:string="/";
        let testValue:string=session.sessId;
        var COOKIE=`${COOKIE_NAME}=${testValue};path=${PATH};`;
        document.cookie=COOKIE;
    }
    public static ValidateSession():void{
        let sessid:string=sessionController.GetCookie();
        this.service.GetRequest<boolean>("User/ValidateSession/"+sessid).subscribe((data) => {
            console.log(data)
        })
        // service.GetRequest<boolean>("User/ValidateSession"+sessid).subscribe((data)=>{
        //     console.log(data);
        //     return data;
        //   });
    }
}