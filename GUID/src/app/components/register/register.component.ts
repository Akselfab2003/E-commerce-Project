import { Component } from '@angular/core';
import {  OnInit, NgModule} from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, ReactiveFormsModule} from '@angular/forms';
import { User } from '../../models/User';
import { matchpassword } from './matchpassword-validator';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  registerForm!: FormGroup;

  ngOnInit(){
    this.registerForm = new FormGroup({
      firstname: new FormControl('', Validators.required),
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


  register() {
    let user:User= new User();
    user.username=this.registerForm.get('firstname')?.value?.toString() as string;
    user.password=this.registerForm.get('password')?.value?.toString() as string;
    user.email=this.registerForm.get('email')?.value?.toString() as string;
    user.gender=this.registerForm.get('gender')?.value as unknown as boolean;
    
    console.log(user.username, user.password,user.email,user.gender);
  }
  showSuccess(): void {
    console.log("You have succesfully made an account!")
  }
}
