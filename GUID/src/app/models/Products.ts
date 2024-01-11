import { Images } from "./Images";
import { ProductVariants } from "./ProductVariants";

export class Products{
    title:string="";
    description:string="";
    price:number=0;
    images:Images[] = new Array<Images>;
    productVariants:ProductVariants[] = new Array<ProductVariants>;
}