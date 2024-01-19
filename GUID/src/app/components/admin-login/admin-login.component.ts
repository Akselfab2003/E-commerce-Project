import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { LoginObject } from '../../models/LoginObject';
import { sessionController } from '../../logic/sessionLogic';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrl: './admin-login.component.css'
})
export class AdminLoginComponent<T> {
  title = "Admin-Login";
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
    this.service.PutRequest<LoginObject>("User/Login/AdminUsers",LoginTry).subscribe((data)=>
      console.log(data));

    this.router.navigate(['/admin-page']);
  }
}
