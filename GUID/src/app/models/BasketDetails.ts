import { ProductVariants } from "./ProductVariants";
import { Products } from "./Products";

export class BasketDetails{
    id:number = 0;
    quantity:number = 1;
    products:Products = new Products;
    variant:ProductVariants = new ProductVariants;
}