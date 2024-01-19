import { Component, OnInit, NgModule} from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, ReactiveFormsModule} from '@angular/forms';
import { User } from '../../models/User';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { environment } from '../../../environments/environment.development';
import { LoginObject } from '../../models/LoginObject';
import { Session } from '../../models/Session';
import { sessionController } from '../../logic/sessionLogic';
import { Router } from '@angular/router';

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

  constructor(private service:HttpserviceService<T>, private router:Router) {
  };


  //starter forfra hvis login ikke passer
  login(){
    let username:string = this.loginForm.get("username")?.value?.toString() as string;
    let password:string = this.loginForm.get("password")?.value?.toString() as string;
    var LoginTry:LoginObject = new LoginObject();
    LoginTry.username = username;
    LoginTry.password =password;
    LoginTry.sessionId =sessionController.GetCookie();
    console.log(LoginTry);
    this.service.PutRequest<Session>("User/Login",LoginTry).subscribe((data)=>
      
      sessionController.SetCookie(data));

    this.router.navigate(['/profile']);
  }
}

