import { Company } from "./Company";
import { User } from "./User";

export class Pricelist{
    id:number = 0;
    PriceListProducts:PriceListEntity[]=[];
    Companies:Company[]=[];
    Users:User[]=[];
}