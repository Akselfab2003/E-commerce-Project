import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { LoginObject } from '../../../models/LoginObject';
import { sessionController } from '../../../logic/sessionLogic';
import { Session } from '../../../models/Session';

@Component({
  selector: 'app-admin-control',
  templateUrl: './admin-control.component.html',
  styleUrl: './admin-control.component.css'
})
export class AdminControlComponent<T> {
  //Gemmer brugerens input i variabler
  createForm = new FormGroup({
    usernameCreate: new FormControl('', Validators.required),
    passwordCreate: new FormControl('', Validators.required),
  });
  updateForm = new FormGroup({
    usernameUpdate: new FormControl('', Validators.required),
    passwordUpdate: new FormControl('', Validators.required),
  })
  deleteForm = new FormGroup({
    usernameDelete: new FormControl(''),
  })
  constructor(private service:HttpserviceService<T>, private router:Router) {
  };

  //starter forfra hvis login ikke passer
  register() {
    let user:LoginObject= this.InputDataCreate();
    this.service.PostRequest<LoginObject>("User/createAdmin",user).subscribe((data)=>
    console.log(data)
    )
  }
  update(){
    let user:LoginObject= this.InputDataUpdate();
    this.service.PutRequest<Session>("User/AdminLogin",user).subscribe((data)=>
    console.log(data)
    );
  }
  delete(){
    let username:string = this.deleteForm.get('usernameDelete')?.value?.toString() as string;
    this.service.DeleteRequest<Session>("User/deleteAdmin/"+username).subscribe((data)=>
    console.log(data)
    );
  }
  InputDataCreate():LoginObject{
    let user:LoginObject=new LoginObject();
    user.username=this.createForm.get('usernameCreate')?.value?.toString() as string;
    user.password=this.createForm.get('passwordCreate')?.value?.toString() as string;
    return user;
  }
  InputDataUpdate():LoginObject{
    let user:LoginObject=new LoginObject();
    user.username=this.updateForm.get('usernameUpdate')?.value?.toString() as string;
    user.password=this.updateForm.get('passwordUpdate')?.value?.toString() as string;
    return user;
  }
}
