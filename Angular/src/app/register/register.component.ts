import { Component, OnInit } from '@angular/core';
import {AccountService} from "../services/account.service";
import {ServerBounderService} from "../services/server-bounder.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  email!: FormControl;
  username!: FormControl;
  password!: FormControl;
  hide = true;

  constructor(
    private accountService:AccountService
  ) {

  }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
    this.email = new FormControl(this.email);
    this.username = new FormControl(this.username);
    this.password = new FormControl(this.password);

    this.registerForm = new FormGroup({
      email: this.email,
      userName: this.username,
      password: this.password
    });
  }

  submitForm(): void {
    let obj = {Email:this.email.value,Username:this.username.value,Password:this.password.value}
    this.accountService.register(obj).subscribe(response =>{
        console.log(response);
    });
  }

}
