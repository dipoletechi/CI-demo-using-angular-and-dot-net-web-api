import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import {RouteHelper} from './../Constant/routerhelper';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import{LocalstorageService}from './../service/Common/localstorage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private routes: Router, private localstorage: LocalstorageService) {
  }
  
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
// debugger;
      if (this.localstorage.getToken()) {     
        return true;
      }
      else {      
        this.routes.navigate([RouteHelper.login]);
        return false;
      }
 
  }
}
