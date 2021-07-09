import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {AccountService} from "../services/account.service";

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {
  userId:string = '';
  code:string = '';
  constructor(private activatedRoute: ActivatedRoute,private confirmEmail:AccountService,private router:Router) {
    this.activatedRoute.queryParams.subscribe(params => {
      this.userId=params['userId'];
      this.code=params['code'];
      console.log(params['userId']); // Print the parameter to the console.
      console.log(params['code']); // Print the parameter to the console.
    });
  }
  result:boolean=false;
  ngOnInit(): void {
    this.confirmEmail.confirmEmail(this.userId,this.code).subscribe(res=>{
      this.result = res;
      if(res){
        this.router.navigateByUrl('/login');

      }else{
        this.router.navigateByUrl('/register');
      }
    });
  }


}
