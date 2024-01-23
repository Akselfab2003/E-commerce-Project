import { Component } from '@angular/core';
import { ProductVariants } from '../../../models/ProductVariants';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { User } from '../../../models/User';
import { validateUserSelected } from '../../../logic/CustomValidators';

@Component({
  selector: 'app-user-control',
  templateUrl: './user-control.component.html',
  styleUrl: './user-control.component.css'
})  
export class UserControlComponent<T> {

   Create!:FormGroup
   Update!:FormGroup
   public UsersList:User[] = new Array<User>();
  constructor(private http:HttpserviceService<T>){

  }
  

  ngOnInit(){
    this.GetListOfUsers()
    this.Create = new FormGroup({
      Username: new FormControl<string>('', Validators.required),
      Password: new FormControl<string>('', Validators.required),
      Email: new FormControl<string>('', [Validators.required, Validators.email]),
      Gender: new FormControl<boolean>(false, Validators.required),
    })

    this.Update = new FormGroup({
      User: new FormControl<string>("",Validators.required),
      Username: new FormControl<string>('', Validators.required),
      Password: new FormControl<string>('', Validators.required),
      Email: new FormControl<string>('', [Validators.required, Validators.email]),
      Gender: new FormControl<boolean>(false, Validators.required),
    },{
      validators:validateUserSelected
    })

    

  }

  GetListOfUsers(){
    this.http.GetRequest<User[]>("User/GetListOfUsers").subscribe(usr=> {
      this.UsersList = usr;
      console.log(usr);
    })

  }


  ParseFormGroupCreate() : User{
    var user:User= new User()
    user.username = this.Create.get("Username")?.value
    user.email = this.Create.get("Email")?.value
    user.gender = this.Create.get("Gender")?.value
    user.password = this.Create.get("Password")?.value
    return user;
  }
  ParseFormGroupUpdate() : User{
    var user:User =  this.UsersList.find(ele => ele.username == this.Update.get("User")?.value) == undefined ? new User() : this.UsersList.find(ele => ele.username == this.Update.get("User")?.value) as User;
    console.log(user)
    user.username = this.Update.get("Username")?.value
    user.email = this.Update.get("Email")?.value
    user.gender = this.Update.get("Gender")?.value
    user.password = this.Update.get("Password")?.value
    return user;
  }

  SubmitCreated() {
    console.log("test")
    let usr:User = this.ParseFormGroupCreate()
    console.log(usr)

    this.http.PostRequest<User>("User/createUser",usr).subscribe(data => {
      console.log(data)
    })
    

  }

  


  SubmitUpdate() {
    console.log("test")
    let usr:User = this.ParseFormGroupUpdate()
    console.log(usr)

    this.http.PutRequest<User>("User/UpdateUser",usr).subscribe(data => {
      console.log(data)
    })
    

  }

}
