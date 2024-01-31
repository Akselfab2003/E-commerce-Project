import { User } from './User';
import { Products } from './Products';

export class Reviews{
    id:number=0;
    UserId:User = new User;
    Products:Products = new Products;
    reviewContent:string = "";
    reviewTitle:string = "";
    reviewRating:number = 0;
}