import { User } from "./User";

export class Company{
    id:number = 0;
    name:string = "";
    cvr:string = "";
    email:string = "";
    users:User[] = new Array<User>();
}