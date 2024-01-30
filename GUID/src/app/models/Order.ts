import { User } from "./User";
import { orderDetails } from "./orderDetails";

export class Order{
    id:number=0;
    users?:User;
    fullname:string= "";
    email:string =  "";
    address:string =  "";
    total:number = 0;
    OrderLines?:orderDetails[] =new Array<orderDetails>();
}