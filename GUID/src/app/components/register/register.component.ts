import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, ReactiveFormsModule} from '@angular/forms';
import { matchpassword } from './matchpassword-validator';
import { HttpClient } from '@angular/common/http';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { User } from '../../models/User';
import { Session} from '../../models/Session'
import { LoginObject } from '../../models/LoginObject';
import { sessionController } from '../../logic/sessionLogic';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent<T> {

  registerForm!: FormGroup;

  ngOnInit(){
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required,Validators.email]),
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required),
      gender: new FormControl('')
    },
    {
      //tjekker om validators passer med matchpassword
      validators:matchpassword
    }
    );
  }
  constructor(private service:HttpserviceService<T>, private router:Router){
  };
  
  
  register() {
    let user:User= this.InputDataUser();
    this.service.PostRequest<User>("User/createUser",user).subscribe((data)=>
    console.log(data)
    )
    let registerObject:LoginObject = this.InputDataObject(sessionController.GetCookie());
    
    this.service.PutRequest<User>("User/Login",registerObject).subscribe((data)=>
    console.log(data));

    this.router.navigateByUrl('/Login');
  }
  InputDataUser():User{
    let user:User=new User();
    user.username=this.registerForm.get('username')?.value?.toString() as string;
    user.password=this.registerForm.get('password')?.value?.toString() as string;
    user.email=this.registerForm.get('email')?.value?.toString() as string;
    if (this.registerForm.get('gender')?.value=="male"){
      user.gender=true;
    }else{
      user.gender=false;
    }
    return user;
  }
  InputDataObject(sessid:string):LoginObject{
    let registerObject:LoginObject = new LoginObject;
    registerObject.username=this.registerForm.get('username')?.value?.toString() as string;
    registerObject.password=this.registerForm.get('password')?.value?.toString() as string;
    registerObject.sessionId=sessid;
    return registerObject;
  }
}