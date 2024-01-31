import { Categories } from "./Categories";
import { Products } from "./Products";
import { Tags } from "./Tags";

export class PriceListEntity{
    id:number = 0;
    priceListPrice:number = 0;
    product:Products = new Products();
}