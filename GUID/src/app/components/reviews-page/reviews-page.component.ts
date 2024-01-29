import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Reviews } from '../../models/Reviews';

@Component({
  selector: 'app-reviews-page',
  templateUrl: './reviews-page.component.html',
  styleUrl: './reviews-page.component.css'
})
export class ReviewsPageComponent {

  newReview: any ={
    Title: "",
    ReviewContent: "",
    rating: 0
  }

  constructor(private http: HttpClient) { }

  addReview(){
    var Review: Reviews = new Reviews();
    Review.Title = this.newReview.Title;
    Review.ReviewContent = this.newReview.ReviewContent;
    Review.Rating = this.newReview.rating;
    console.log(Review);

    this.http.post<Reviews>("Reviews/CreateReviews",Review).subscribe((data)=>
    console.log(data)
    
    );

  }
}




