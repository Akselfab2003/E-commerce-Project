import { Categories } from "./Categories";
import { Images } from "./Images";
import { ProductVariants } from "./ProductVariants";

export class Products{
    id:number=0;
    title:string="";
    description:string="";
    price:number=0;
    images:Images[] = new Array<Images>;
    Quantity:number=1;
    productVariants:ProductVariants[] = new Array<ProductVariants>;
    productCategories:Categories = new Categories;
    Active:boolean = false;
}