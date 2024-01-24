import { Component } from '@angular/core';
import { ProductVariants } from '../../../models/ProductVariants';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { User } from '../../../models/User';
import { validateUserSelected } from '../../../logic/CustomValidators';

@Component({
  selector: 'app-user-control',
  templateUrl: './user-control.component.html',
  styleUrl: './user-control.component.css',
  
})  
export class UserControlComponent<T> {

   Create!:FormGroup
   Update!:FormGroup
   Delete!:FormGroup

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
    });
    this.Update.controls["User"].valueChanges.subscribe( value =>
        {
        if(value !=""){

          this.SelectOnChange(value)
        }
        });
    this.Delete = new FormGroup({
      User: new FormControl<string>("",Validators.required),
    },{
      validators:validateUserSelected
    })
    

  }

  GetListOfUsers(){
    this.http.GetRequest<User[]>("User/GetListOfUsers").subscribe(usr=> {
      this.UsersList = usr;
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
  ParseFormGroupUpdate() : User {
    var user:User =  this.UsersList.find(ele => ele.username == this.Update.get("User")?.value) == undefined ? new User() : this.UsersList.find(ele => ele.username == this.Update.get("User")?.value) as User;
    user.username = this.Update.get("Username")?.value
    user.email = this.Update.get("Email")?.value
    user.gender = this.Update.get("Gender")?.value
    user.password = this.Update.get("Password")?.value
    return user;
  }
  ParseFormGroupDelete() : User{
    var user:User =  this.UsersList.find(ele => ele.username == this.Delete.get("User")?.value) == undefined ? new User() : this.UsersList.find(ele => ele.username == this.Delete.get("User")?.value) as User;
    return user;
  }

  SubmitCreated() {
    let usr:User = this.ParseFormGroupCreate()

    this.http.PostRequest<User>("User/createUser",usr).subscribe(data => {
      if(data != null){
        this.Create.reset()
        this.GetListOfUsers()

      }
    })
    

  }

  SubmitUpdate() {
    let usr:User = this.ParseFormGroupUpdate()

    this.http.PutRequest<User>("User/UpdateUser",usr).subscribe(data => {
      if(data != null){
        this.Update.reset()
        this.GetListOfUsers()
      }
    })
    

  }

  SubmitDelete() {
      let usr:User = this.ParseFormGroupDelete()
      this.http.PostRequest<User>("User/DeleteUser",usr).subscribe(data => {
        if(data != null){
          this.Delete.reset()
          this.GetListOfUsers()
        }
      })
      
  
  }
  SelectOnChange(value:string):void{

    var user:User =this.UsersList.find(ele => ele.username == value) == undefined ? new User() : this.UsersList.find(ele => ele.username == value) as User;
   
    if(user != new User()){
       this.Update.setValue({User:user.username,Username:user.username,Email:user.email,Password:user.password,Gender:user.gender},{emitEvent:false})
     
    }
  }


}
