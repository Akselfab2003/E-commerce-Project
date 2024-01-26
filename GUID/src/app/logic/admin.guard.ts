import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { inject } from '@angular/core';
import { HttpserviceService } from '../../Services/httpservice.service';
import { adminController } from './adminLogic';


export const adminGuard: CanActivateFn = (
  next:ActivatedRouteSnapshot,
  state:RouterStateSnapshot) => {
    const httpservice:HttpserviceService<any> = inject(HttpserviceService)
    var test = adminController.ValidateSession(httpservice).then(ele =>{
      return ele
    })  
     console.log(test)
    return test;
}
