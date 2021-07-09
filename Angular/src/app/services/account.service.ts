import { Injectable } from '@angular/core';
import {FanficRatingFandomTemplate} from "../model/FanficRatingFandomTemplate";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private httpClient:HttpClient) { }
  register(param:any):Observable<boolean>{
    console.log(param);
    return this.httpClient.post<boolean>('https://localhost:44391/account/register',param);
  }
  contr():Observable<string>{
    return this.httpClient.post<string>('https://localhost:44391/account/contr',"string");
  }
  confirmEmail(userId:string,code:string):Observable<boolean>{
    return this.httpClient.get<boolean>('https://localhost:44391/account/confirmEmail',{params:{userId:userId,code:code}});
  }
  login(obj:any):Observable<string>{
    return this.httpClient.post<string>('https://localhost:44391/account/login',obj);
  }
}
