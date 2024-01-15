import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { sessionController } from './sessionLogic';
import { HttpserviceService } from '../../Services/httpservice.service';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { inject } from '@angular/core';
export const authenticatorGuard: CanActivateFn = (
  next:ActivatedRouteSnapshot,
  state:RouterStateSnapshot) => {
  return sessionController.WaitMethod();
};
