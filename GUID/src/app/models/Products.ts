import { Images } from "./Images";
import { ProductVariants } from "./ProductVariants";

export class Products{
    Title:string="";
    Description:string="";
    Price:number=0;
    Images:Images[] = new Array<Images>;
    ProductVariants:ProductVariants[] = new Array<ProductVariants>;
}