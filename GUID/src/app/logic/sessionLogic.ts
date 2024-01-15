import { AbstractControl } from "@angular/forms";
import { HttpserviceService } from "../../Services/httpservice.service";
import { Session } from "../models/Session"
import { inject } from "@angular/core";

export class sessionController{
    constructor(){
    }
    private static validated:boolean = false;
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
    public static WaitMethod():boolean{
        this.ValidateSession()
        return sessionController.validated;
    }
     public static ValidateSession(){
        let sessid:string=sessionController.GetCookie();
        console.log(sessid)
        console.log()
        HttpserviceService.GetRequest<boolean>("User/ValidateSession"+sessid).subscribe((data) => {
            console.log(data)
            sessionController.validated = data;
     })
       
    }
}