import { Component } from '@angular/core';
import { Products } from '../../models/Products';
import { HttpClient } from '@angular/common/http';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { basketLogic } from '../../logic/basketLogic';
import { sessionController } from '../../logic/sessionLogic';

@Component({
  selector: 'app-carousel-reviews',
  templateUrl: './carousel-reviews.component.html',
  styleUrl: './carousel-reviews.component.css'
})
export class CarouselReviewsComponent<T> {
  CurrentProductsDisplayedOnPage: Products[] = new Array<Products>();

  constructor(private http: HttpClient, private httpService: HttpserviceService<T>, private basketTest:basketLogic<T>) {}
  
  ngOnInit(): void {
    this.fetchDataFromApi();
  }

  fetchDataFromApi() {
    this.httpService.GetRequest<Products[]>(`Products/GetLimitedAmountOfProducts/${sessionController.GetCookie()}`).subscribe((data) => {
      this.CurrentProductsDisplayedOnPage = data;
      console.log(data);
    });
  };

  AddToBasket(product: Products){
    this.basketTest.AddToBasket(product)
  }
  

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
