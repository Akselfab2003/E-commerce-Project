import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { sessionController } from './sessionLogic';

export const authenticatorGuard: CanActivateFn = (
  next:ActivatedRouteSnapshot,
  state:RouterStateSnapshot) => {
    console.log(`Before ${Date.now()}`)
    var test:boolean = sessionController.WaitMethod()
    console.log(`After ${Date.now()}`)

  return test;
};

export const sessionGuard: CanActivateFn = (
  next:ActivatedRouteSnapshot,
  state:RouterStateSnapshot) => {
    console.log(sessionController.GetCookie())
    if (sessionController.GetCookie()==undefined){
      sessionController.WaitMethodEmptySession()
    }
    return true;
};
