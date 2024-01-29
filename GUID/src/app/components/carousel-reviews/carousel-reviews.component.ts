import { Component } from '@angular/core';
import { Products } from '../../models/Products';
import { HttpClient } from '@angular/common/http';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { sessionController } from '../../logic/sessionLogic';
import { Reviews } from '../../models/Reviews';

@Component({
  selector: 'app-carousel-reviews',
  templateUrl: './carousel-reviews.component.html',
  styleUrl: './carousel-reviews.component.css'
})
export class CarouselReviewsComponent<T> {
  CurrentReviewDisplayedOnPage: Reviews[] = new Array<Reviews>();

  constructor(private http: HttpClient, private httpService: HttpserviceService<T>) {}
  
  ngOnInit(): void {
    this.fetchDataFromApi();
  }

  fetchDataFromApi() {
    this.httpService.GetRequest<Reviews[]>(`Reviews/Get_Reviews1?${sessionController.GetCookie()}`).subscribe((data) => {
      this.CurrentReviewDisplayedOnPage = data;
      console.log(data);
    });
  };
  

  slideConfig = {
    "slidesToShow": 4, 
    "slidesToScroll": 4,
    "autoplay": true,
    "autoplaySpeed": 5000,
    "dots": true,
    "pauseOnHover": true,
    "infinite": true,
    "responsive": [
        {
        "breakpoint": 992,
        "settings": {
          "arrows": true,
          "infinite": true,
          "slidesToShow": 3,
          "slidesToScroll": 3
        }
      },
      {
        "breakpoint": 768,
        "settings": {
          "arrows": true,
          "infinite": true,
          "slidesToShow": 1,
          "slidesToScroll": 1
        }
      }
    ]
  };
}
