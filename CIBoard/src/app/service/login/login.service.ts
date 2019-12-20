import { Injectable } from '@angular/core';
import {ApiService} from './../Common/api.service';
import {UrlHelperService} from './../Common/urlhelper.service';
import {LoginModel} from './../../models/login/login.model'

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private _api:ApiService, private loginmodel : LoginModel) { }


  login(logindetail)
  {
    return this._api.post(UrlHelperService.login,logindetail)
  }

}
