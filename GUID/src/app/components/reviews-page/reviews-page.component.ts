import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { Reviews } from '../../models/Reviews';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Products } from '../../models/Products';
import { ActivatedRoute, Router } from '@angular/router';
import { sessionController } from '../../logic/sessionLogic';


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

  constructor(private http: HttpClient, private service:HttpserviceService<T>, private route:ActivatedRoute, private router:Router) { }

  createReview(){
    let review:Reviews = new Reviews();
    review.Products = this.product;
    review.reviewTitle = this.newReviewForm.get('ReviewTitle')?.value as string;
    review.reviewContent = this.newReviewForm.get('ReviewContent')?.value as string;
    review.reviewRating = this.newReviewForm.get('ReviewRating')?.value as number;

    return review;
  };

  addReview(){
    this.createReview();
    let sessId: string = sessionController.GetCookie();
    let review:Reviews = this.createReview();
    this.service.PostRequest<any>(`Reviews/CreateReviews/${sessId}`,review).subscribe((data)=>{

      console.log(data)
      this.router.navigateByUrl(`/product-details/${this.product.id}`);
    }
    )
  };

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

  selectedRating: number = 0;

  handleRatingSelection(rating: number) {
    this.selectedRating = rating;
    console.log("Selected rating:", this.selectedRating);
  }

 /*  submitReview() {
    if (this.selectedRating > 0) {
      console.log("Submitting rating:", this.selectedRating);
      this.selectedRating = 0; // Reset selectedRating
    } else {
      console.log("Please select a rating before submitting.");
    }
  } */

  /* addRating( addEventListener: any){
    const ratingContainer = document.getElementById("rating");
  const submitBtn = document.getElementById("submitBtn");
  let selectedRating: number = 0;

  if (ratingContainer && submitBtn) {
    ratingContainer.addEventListener("click", (event) => {
      const target = event.target as HTMLInputElement;
      if (target.type === "radio") {
        selectedRating = parseInt(target.value);
        console.log("Selected rating:", selectedRating);
      }
    });

    submitBtn.addEventListener("click", () => {
      if (selectedRating > 0) {
        console.log("Submitting rating:", selectedRating);
        selectedRating = 0; // Reset selectedRating
      } else {
        console.log("Please select a rating before submitting.");
      }
    });
    }
  } */
}




