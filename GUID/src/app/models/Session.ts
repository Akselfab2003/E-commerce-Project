import { User } from "./User";
export class Session{
    id:number=0;
    sessid:string="";
    user:User= new User;
    created:Date= new Date;
}