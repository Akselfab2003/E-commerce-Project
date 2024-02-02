import { Component, Input, OnInit } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { sessionController } from '../../logic/sessionLogic';
import { SlickCarouselComponent } from 'ngx-slick-carousel';
import { HttpClient } from '@angular/common/http';
import { Products } from '../../models/Products';
import { basketLogic } from '../../logic/basketLogic';


@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css'
})
export class CarouselComponent<T> implements OnInit {

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
    event?.stopPropagation()
    this.basketTest.AddToBasket(product,undefined,1)
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

