import { Component, OnInit } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import{RouteHelper} from './../../../Constant/routerhelper';
import {LoginService} from './../../../service/login/login.service'; 
import {LocalstorageService} from './../../../service/Common/localstorage.service';

@Component({
  selector: 'app-appheader',
  templateUrl: './appheader.component.html',
  styleUrls: ['./appheader.component.css']
})
export class AppheaderComponent implements OnInit {
 
  logindetail: any;
  loginResponse: any;
  name: string;

  constructor(private localstorage: LocalstorageService,private _loginservice: LoginService ,private router: Router) {
    this.name=this.localstorage.getname();
   }
  ngOnInit() {

  }

logout() {
    localStorage.removeItem('loginToken');
    console.log("function header");
    this.localstorage.logout();
    this.localstorage.changeLoginStatus(false);
    this.router.navigate([RouteHelper.login]);

  
   
    
  }
}
