import { firstValueFrom, take } from "rxjs";
import { HttpserviceService } from "../../Services/httpservice.service";
import { sessionController } from "../logic/sessionLogic";
import { inject } from '@angular/core';
import { runInInjectionContext } from '@angular/core';

export class adminController{
    private static validated:boolean = false;
    public static   async ValidateSession(httpservice:HttpserviceService<any>) : Promise<boolean>{
        let sessid:string=sessionController.GetCookie();
        var test =   await firstValueFrom<boolean>(httpservice.GetRequest<boolean>("User/ValidateSessionAdmin/"+sessid).pipe(take(1)));

        
        return test;
    }
}