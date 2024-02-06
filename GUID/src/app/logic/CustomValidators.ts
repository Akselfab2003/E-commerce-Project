import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { User } from "../models/User";


export const  validateUserSelected:ValidatorFn = 
     (control: AbstractControl) : ValidationErrors | null => {
    var usr = control.get("User");
    
    
    if(usr?.value == ""){
        return{
            ErrorState:true,
            showError(): void{
                 
    
            }
        }

    }else{
        return null
    }
    
}