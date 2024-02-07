import { Products } from "./Products";

export class ProductVariants{
    id:number=0;
    parentProduct:Products = new Products;
    name:String="";
    description:String="";
    price:number=0;
    variantValue:string = "";
}