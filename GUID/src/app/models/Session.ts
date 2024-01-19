import { User } from "./User";

export class Session{
    id:number=0;
    sessId:string="";
    created:Date= new Date;
    user?:User;
}