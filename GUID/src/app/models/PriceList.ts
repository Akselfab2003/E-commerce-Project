import { Company } from "./Company";
import { PriceListEntity } from "./PriceListEntity";
import { User } from "./User";

export class Pricelist{
    id:number = 0;
    name:string="";
    priceListProducts:PriceListEntity[]=[];
    companies:Company[]=[];
    users:User[]=[];
}