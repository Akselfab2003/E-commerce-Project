import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { User } from "../models/User";


export const  validateUserSelected:ValidatorFn = 
     (control: AbstractControl) : ValidationErrors | null => {
    var usr = control.get("User");
    console.log(usr?.value  )
    
    
    if(usr?.value == ""){
        return{
            ErrorState:true,
            showError(): void{
                console.log("dont work")
    
            }
        }

    }else{
        console.log("returned")
        return null
    }
    
}