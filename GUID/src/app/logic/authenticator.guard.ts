import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { sessionController } from './sessionLogic';
import { inject } from '@angular/core';
import { HttpserviceService } from '../../Services/httpservice.service';

export const authenticatorGuard: CanActivateFn = (
  next:ActivatedRouteSnapshot,
  state:RouterStateSnapshot) => {
    const httpservice:HttpserviceService<any> = inject(HttpserviceService)
    var test:boolean =  sessionController.ValidateSession(httpservice)

  return test;
};

