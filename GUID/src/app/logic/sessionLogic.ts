import { Observable, asyncScheduler, firstValueFrom, scheduled } from "rxjs";
import { HttpserviceService } from "../../Services/httpservice.service";
import { Session } from "../models/Session"

export class sessionController <T> {


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
        console.log(document.cookie)
    }

 
    public static   ValidateSession(httpservice:HttpserviceService<any>) : boolean{
        let sessid:string=sessionController.GetCookie();
    
        httpservice.GetRequest<boolean>("User/ValidateSession/"+sessid).subscribe(
            (data) => { sessionController.validated = data; }
           
            )

        console.log(sessionController.validated)
        return sessionController.validated;
    }

   

    public static  CreateEmptySession(httpservice:HttpserviceService<any>){
        httpservice.GetRequest<Session>("User/createEmptySession").subscribe((data) => {
            console.log(data)
            sessionController.SetCookie(data);
         });
    }
}