import { HttpserviceService } from "../../Services/httpservice.service";
import { sessionController } from "../logic/sessionLogic";
import { inject } from '@angular/core';
import { runInInjectionContext } from '@angular/core';

export class adminController{
    private static validated:boolean = false;
    public static   ValidateSession(httpservice:HttpserviceService<any>) : boolean{
        let sessid:string=sessionController.GetCookie();
    
        httpservice.GetRequest<boolean>("User/ValidateSession/"+sessid).subscribe(
            (data) => {
                 this.validated = data;
                 console.log(data);
             }
            )
        return this.validated;
    }
}