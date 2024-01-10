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
  ArrayOfWeatherData:Array<any> = Array();
  constructor(http:HttpserviceService,){
    this.httpService = http;

  }

  ngOnInit(){
    this.sendrequest();

  }


  sendrequest(){
    var int:number = 0;
    var Test = new  HttpModule("https://localhost:7094/WeatherForecast",RequestType.Get,{})
    this.httpService.CreateHttpRequest(Test).subscribe(
      ele => {
        console.log(ele)
        
      }
    );
  }




}
class TestTest {
  constructor() {
    
  }
  date:object =  {
  }
  //date:string = ""
  temperatureC:number = 0
  temperatureF:number = 0
  summary:string = ""


}