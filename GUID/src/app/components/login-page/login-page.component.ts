import { Component, OnInit, NgModule} from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, ReactiveFormsModule} from '@angular/forms';
import { User } from '../../models/User';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { environment } from '../../../environments/environment.development';
import { LoginObject } from '../../models/LoginObject';

@Component({
  selector: 'app-login',
  templateUrl: './Login-page.component.html',
  styleUrls: ['./Login-page.component.css']
})
export class LoginComponent<T> {
  title = "Login-page";
  //Gemmer brugerens input i variabler
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  constructor(private service:HttpserviceService<T>){
  };


  //starter forfra hvis login ikke passer
  login(){
  
    let username:string = this.loginForm.get("username")?.value?.toString() as string;
    let password:string = this.loginForm.get("password")?.value?.toString() as string;
    var LoginTry:LoginObject = new LoginObject();
    LoginTry.username = username;
    LoginTry.password =password;
     
    this.service.PostRequest<LoginObject>("User/Test",LoginTry).subscribe((data)=>
    console.log(data)
    )
    if(this.loginForm.invalid) return;
  }
}

