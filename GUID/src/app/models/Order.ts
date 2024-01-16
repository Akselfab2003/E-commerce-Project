import { User } from "./User";

export class Order{
    id:number=0;
    users?:User;
    orderDetails?:string;
}