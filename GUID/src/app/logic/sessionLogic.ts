import { Session } from "../models/Session"

export class sessionController{
    public GetCookie():Session{
        return new Session();
    }
    public static SetCookie(session:Session){
        const COOKIE_NAME:string="sessionId";
        const PATH:string="/";
        let testValue:string=session.sessId;
        let expireDate:Date = new Date;
        expireDate.setHours(expireDate.getHours()+2);

        var COOKIE=`${COOKIE_NAME}=${testValue}; expires=${expireDate}; path=${PATH};`;
        document.cookie+=COOKIE;
        console.log(COOKIE)
        console.log(session);
        
        return true;
    }
}