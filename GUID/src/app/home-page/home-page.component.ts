import { Component } from '@angular/core';
import { HttpserviceService } from '../../Services/httpservice.service';
import { HttpModule } from '../Functions modules/HttpModule';
import { RequestType } from '../Functions modules/request-type';
@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
  providers:[HttpserviceService]
})
export class HomePageComponent {
  private httpService:HttpserviceService;
  constructor(http:HttpserviceService){
    this.httpService = http;
    this.sendrequest();
  }

  sendrequest(){
    var Test = new  HttpModule("https://localhost:7094/WeatherForecast",RequestType.Get,{})
    var result = this.httpService.CreateHttpRequest(Test)

    console.log(result)
  }
}
