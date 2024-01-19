import { Session } from "./Session";
import { User } from "./User";
import {BasketDetails} from "./BasketDetails"

export class Basket{
    id:number = 0;
    session:Session = new Session;
    basketDetails:BasketDetails[] = new Array<BasketDetails>;
}