import { User } from "./User";
import { orderDetails } from "./orderDetails";

export class Order{
    id:number=0;
    users?:User;
    email:string =  "";
    fullName:string= "";
    address:string =  "";
    total:number = 0;
    orderLines:orderDetails[] =new Array<orderDetails>();
}