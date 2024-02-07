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

   // Array to hold the list of User objects
   public UsersList:User[] = new Array<User>();
  constructor(private http:HttpserviceService<T>){

  }
  

  ngOnInit(){
    this.GetListOfUsers()
    // Initialization of Create FormGroup with form controls for Username, Password, Email, and Gender
    this.Create = new FormGroup({
      Username: new FormControl<string>('', Validators.required),
      Password: new FormControl<string>('', Validators.required),
      Email: new FormControl<string>('', [Validators.required, Validators.email]),
      Gender: new FormControl<boolean>(false, Validators.required),
    })
    // Initialization of Update FormGroup with form controls for User, Username, Password, Email, and Gender
    this.Update = new FormGroup({
      User: new FormControl<string>("",Validators.required),
      Username: new FormControl<string>('', Validators.required),
      Password: new FormControl<string>('', Validators.required),
      Email: new FormControl<string>('', [Validators.required, Validators.email]),
      Gender: new FormControl<boolean>(false, Validators.required),
    },{
      validators:validateUserSelected
    });
    // checks for value changes of the 'User' control in the Update form
    this.Update.controls["User"].valueChanges.subscribe( value =>
        {
        if(value !=""){

          this.SelectOnChange(value)
        }
        });
    // Initialization of Delete FormGroup with a form control for User
    this.Delete = new FormGroup({
      User: new FormControl<string>("",Validators.required),
    },{
      validators:validateUserSelected
    })
  }

  //gets a list of all users from the database
  GetListOfUsers(){
    this.http.GetRequest<User[]>("User/GetListOfUsers").subscribe(usr=> {
      this.UsersList = usr;
    })

  }

//creates a new User object from the values in the Create form and returns it.
  ParseFormGroupCreate() : User{
    var user:User= new User()
    user.username = this.Create.get("Username")?.value
    user.email = this.Create.get("Email")?.value
    user.gender = this.Create.get("Gender")?.value
    user.password = this.Create.get("Password")?.value
    return user;
  }
  //finds a User object in the UsersList that matches the username in the Update form, updates its values with the form values, and returns it.
  ParseFormGroupUpdate() : User {
    var user:User =  this.UsersList.find(ele => ele.username == this.Update.get("User")?.value) == undefined ? new User() : this.UsersList.find(ele => ele.username == this.Update.get("User")?.value) as User;
    user.username = this.Update.get("Username")?.value
    user.email = this.Update.get("Email")?.value
    user.gender = this.Update.get("Gender")?.value
    user.password = this.Update.get("Password")?.value
    return user;
  }
  //finds a User object in the UsersList that matches the username in the Delete form and returns it.
  ParseFormGroupDelete() : User{
    var user:User =  this.UsersList.find(ele => ele.username == this.Delete.get("User")?.value) == undefined ? new User() : this.UsersList.find(ele => ele.username == this.Delete.get("User")?.value) as User;
    return user;
  }

//calls the ParseFormGroupCreate method to get a User object, sends a POST request to the server to create the user, and then resets the Create form and fetches the updated list of users.
  SubmitCreated() {
    let usr:User = this.ParseFormGroupCreate()

    this.http.PostRequest<User>("User/createUser",usr).subscribe(data => {
      if(data != null){
        this.Create.reset()
        this.GetListOfUsers()
      }
    })
  }

  //calls the ParseFormGroupUpdate method to get a User object, sends a PUT request to the server to update the user, and then resets the Update form and fetches the updated list of users.
  SubmitUpdate() {
    let usr:User = this.ParseFormGroupUpdate()

    this.http.PutRequest<User>("User/UpdateUser",usr).subscribe(data => {
      if(data != null){
        this.Update.reset()
        this.GetListOfUsers()
      }
    })
  }

  //calls the ParseFormGroupDelete method to get a User object, sends a POST request to the server to delete the user, and then resets the Delete form and fetches the updated list of users.
  SubmitDelete() {
      let usr:User = this.ParseFormGroupDelete()
      this.http.PostRequest<User>("User/DeleteUser",usr).subscribe(data => {
        if(data != null){
          this.Delete.reset()
          this.GetListOfUsers()
        }
      })
  }

  //triggered when the selected value in the Update form changes
  SelectOnChange(value:string):void{

    var user:User =this.UsersList.find(ele => ele.username == value) == undefined ? new User() : this.UsersList.find(ele => ele.username == value) as User;
   
    if(user != new User()){
       this.Update.setValue({User:user.username,Username:user.username,Email:user.email,Password:user.password,Gender:user.gender},{emitEvent:false})
    }
  }
}
