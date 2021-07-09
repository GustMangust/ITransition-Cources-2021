import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Fanfic} from "../model/Fanfic";
import {Fandom} from "../model/Fandom";
import { FanficRatingFandomTemplate} from "../model/FanficRatingFandomTemplate";

@Injectable({
  providedIn: 'root'
})
export class ServerBounderService {

  constructor(private httpClient:HttpClient) {

  }
  fanfics():Observable<Array<Array<FanficRatingFandomTemplate>>>{

    return this.httpClient.get<Array<Array<FanficRatingFandomTemplate>>>('https://localhost:44391/');
  }
  boundMethod(id:number):Observable<Array<Fanfic>>{
    id = 7;
    return this.httpClient.get<Array<Fanfic>>('https://localhost:44391/Fanfic/EditFanfic/'+id);
  }
}
