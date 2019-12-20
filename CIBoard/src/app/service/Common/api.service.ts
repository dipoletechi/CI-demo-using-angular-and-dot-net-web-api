import { Injectable, Inject } from '@angular/core';
import { Headers, Http, Response, URLSearchParams ,RequestOptions} from '@angular/http'
import { Observable } from 'rxjs';
// import { _throw } from 'rxjs/observable/throw';
// import 'rxjs/add/operator/map';
// import 'rxjs/add/operator/catch';
import { environment } from '../../../environments/environment';
import {LocalstorageService} from './localstorage.service';
import { HttpParameterCodec, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
   url:any;
  constructor(
    @Inject(Http)
    private http: Http,
    private localstorage:LocalstorageService
  ) {}

   
  private setHeaders(isAuthorize:boolean,isContentAllowed:boolean=true): Headers {
       
    var accessToken = this.localstorage.getToken();
 

    const headersConfig = {
      
      'Accept': 'application/json',
    };

    if(isContentAllowed){
      headersConfig["Content-Type"]="application/json";
    }
    if(isAuthorize){
      if (accessToken) {
        headersConfig['Authorization'] = accessToken;      
      }       
    }
  
    return new Headers(headersConfig);
  }
  
  get(path: string, params: URLSearchParams = new URLSearchParams(),isAuthorize:boolean=true): Observable<any> {
    return this.http.get(`${environment.base_url}${path}`, { headers: this.setHeaders(isAuthorize), search: params });
  }
  

  post(path: string, body: Object = {},isAuthorize:boolean=true): Observable<any> {
    return this.http.post(`${environment.base_url}${path}`, JSON.stringify(body), { headers: this.setHeaders(isAuthorize) });
  }

  put(path: string, body: object = {},isAuthorize:boolean=true): Observable<any> {
    return this.http.put(`${environment.base_url}${path}`, JSON.stringify(body), { headers: this.setHeaders(isAuthorize) });
  }

  delete(path: string, isAuthorize:boolean=true,isContentAllowed:boolean=true): Observable<any> {
    return this.http.delete(`${environment.base_url}${path}`, { headers: this.setHeaders(isAuthorize, isContentAllowed) });
  }


//    uploadImage ( file, imageUrlData) {  
//     const headers = new Headers({'Content-Type': 'image/jpeg'});
//     const options = new RequestOptions({ headers: headers });
//     var uploadUrl = imageUrlData.presigned_url;    
//     return this.http.put(uploadUrl,file ,options)              
    
// }
 
}