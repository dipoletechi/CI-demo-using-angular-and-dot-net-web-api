import { Injectable } from '@angular/core';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalstorageService {
  public IsLogin : Subject<boolean> = new Subject<boolean>();
  constructor() { }

  
setToken(token:string){
  localStorage.setItem("loginToken", token)  
 }

 setname(name:string){
   localStorage.setItem("loginname", name);
 }

 getname(){
  return localStorage.getItem("loginname");
 }

 getToken(){
  return localStorage.getItem("loginToken");
 }

 setLanguage(languageId:string)
 {
   localStorage.setItem("languageId",languageId)   
 }

 getLanguage()
 {
  return localStorage.getItem("languageId");
 }
 
islogin()
{
  if(this.getToken()){    
    this.IsLogin.next(true)
  }else{
    this.IsLogin.next(false)
  }
}

changeLoginStatus(_currentIsLogin: boolean) {
  
  this.IsLogin.next(_currentIsLogin)
}

logout(){
  localStorage.setItem("loginToken","");
}




}
