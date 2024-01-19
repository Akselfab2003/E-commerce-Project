import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { inject } from '@angular/core';
import { HttpserviceService } from '../../Services/httpservice.service';
import { adminController } from './adminLogic';


export const adminGuard: CanActivateFn = (
  next:ActivatedRouteSnapshot,
  state:RouterStateSnapshot) => {
    const httpservice:HttpserviceService<any> = inject(HttpserviceService)
    var test:boolean = adminController.ValidateSession(httpservice);

  return test;
}
