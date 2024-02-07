import { ProductVariants } from "./ProductVariants";
import { Products } from "./Products";

export class orderDetails{
    id:number=0;
    product?:Products = new Products();
    variant?:ProductVariants = new ProductVariants();
    price:number=0;
    quantity:number=0;
    total:number=0;
}