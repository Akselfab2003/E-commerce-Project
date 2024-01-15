import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { sessionController } from './sessionLogic';
import { HttpserviceService } from '../../Services/httpservice.service';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { inject } from '@angular/core';
import { Session } from '../models/Session';
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
    if (sessionController.GetCookie()==undefined){
      HttpserviceService.PostRequest<Session>("User/empty").subscribe((data) => {
        sessionController.SetCookie(data);
      });
    }
    return true;
};
