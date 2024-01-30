import { User } from './User';
import { Products } from './Products';

export class Reviews{
    id:number=0;
    UserId:User = new User;
    Products:Products = new Products;
    ReviewContent:string = "";
    ReviewTitle:string = "";
    ReviewRating:number = 0;
}