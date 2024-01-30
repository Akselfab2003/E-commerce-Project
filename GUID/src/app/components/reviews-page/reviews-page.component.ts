import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { Reviews } from '../../models/Reviews';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Products } from '../../models/Products';
import { ActivatedRoute } from '@angular/router';




@Component({
  selector: 'app-reviews-page',
  templateUrl: './reviews-page.component.html',
  styleUrl: './reviews-page.component.css'
})
export class ReviewsPageComponent<T> {

  @Input() product: Products = new Products();
  productReviews: Reviews[] = new Array<Reviews>();

  newReviewForm = new FormGroup({
    ReviewTitle: new FormControl<string>('', Validators.required),
    ReviewContent: new FormControl<string>('', Validators.required),
    ReviewRating: new FormControl<number>(1, Validators.required),
  })

  constructor(private http: HttpClient, private service:HttpserviceService<T>, private route:ActivatedRoute) { }

  createReview(){
    let review:Reviews = new Reviews();
    review.ReviewTitle = this.newReviewForm.get('ReviewTitle')?.value as string;
    review.ReviewContent = this.newReviewForm.get('ReviewContent')?.value as string;
    review.ReviewRating = this.newReviewForm.get('ReviewRating')?.value as number;

    console.log(review);
    return review;
  }

  addReview(){
    let review:Reviews = this.createReview();
    this.service.PostRequest<Reviews>("Reviews" + (this.newReviewForm.get('ReviewTitle')?.value as unknown as string),review).subscribe((data)=>
    console.log(data)
    )
  }

  GetProduct<T>(id:Number){
    this.service.GetRequest<Products>(`Products/${id}`).subscribe((data)=>{
      this.product = data;
      console.log(data)
    });
  };

  ngOnInit(): void {
    this.route.paramMap.subscribe((data)=>{
      var selectedId = Number(data.get('id'));

      console.log("ProductDetails Object:");
      console.log(selectedId);

      this.GetProduct(selectedId);
    })
  }
}




