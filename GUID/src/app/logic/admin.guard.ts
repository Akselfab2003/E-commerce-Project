import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { inject } from '@angular/core';
import { HttpserviceService } from '../../Services/httpservice.service';


export const adminGuard: CanActivateFn = (
  next:ActivatedRouteSnapshot,
  state:RouterStateSnapshot) => {
    const httpservice:HttpserviceService<any> = inject(HttpserviceService)
    return true;
}
