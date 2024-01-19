import { Component } from '@angular/core';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { sessionController } from '../../logic/sessionLogic';
import { SlickCarouselComponent } from 'ngx-slick-carousel';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css'
})
export class CarouselComponent {
  
  slides = [
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""},
    {img: ""}
  ];

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
