import { Observable, Subject, asyncScheduler, firstValueFrom, scheduled, take, takeUntil } from "rxjs";
import { HttpserviceService } from "../../Services/httpservice.service";
import { Session } from "../models/Session";

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
      
    }

    public static   async ValidateSession(httpservice:HttpserviceService<any>) : Promise<boolean>{
        let sessid:string= this.GetCookie();
        var test =   await firstValueFrom<boolean>(httpservice.GetRequest<boolean>("User/ValidateSession/"+sessid).pipe(take(1)));
        return test;
    }

    public static  CreateEmptySession(httpservice:HttpserviceService<any>){
        httpservice.GetRequest<Session>("User/createEmptySession").subscribe((data) => {
            sessionController.SetCookie(data);
         });
    }
}