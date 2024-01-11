import { Component } from '@angular/core';
import {  OnInit, NgModule, EventEmitter} from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, ReactiveFormsModule} from '@angular/forms';
import { matchpassword } from './matchpassword-validator';
import { HttpClient } from '@angular/common/http';
import { HttpserviceService } from '../../../Services/httpservice.service';
import { User } from '../../models/User';


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
  constructor(private service:HttpserviceService<T>){
    this.service.GetRequest<User>("User").subscribe()
    };


  register<T>() {
    // let user:User= new User();
    // user.username=this.registerForm.get('firstname')?.value?.toString() as string;
    // user.password=this.registerForm.get('password')?.value?.toString() as string;
    // user.email=this.registerForm.get('email')?.value?.toString() as string;
    // user.gender=this.registerForm.get('gender')?.value as unknown as boolean;

    // this.service.PostRequest<User>("User",user).subscribe((data)=>
    // console.log(data)
    // )

  }
  showSuccess(): void {
    console.log("You have succesfully made an account!")
  }
}
