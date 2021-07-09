import { Component, OnInit } from '@angular/core';
import {AccountService} from "../services/account.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private accountService:AccountService) { }
loginResult:string='';
  ngOnInit(): void {

  }
  login(email:string,password:string){
    this.accountService.login({Email:email,Password:password,ReturnUrl:''}).subscribe(res=>{
      this.loginResult = res;
    })
  }
}
