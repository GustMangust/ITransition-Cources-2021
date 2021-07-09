import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {FanficsComponent} from "./fanfics/fanfics.component";
import {RegisterComponent} from "./register/register.component";
import {AppComponent} from "./app.component";
import {ConfirmEmailComponent} from "./confirm-email/confirm-email.component";
import {LoginComponent} from "./login/login.component";

const routes: Routes = [
  { path:'',component: AppComponent},
  { path:'confirmEmail',component: ConfirmEmailComponent},
  { path: 'fanfics', component: FanficsComponent },
  { path: 'register',component: RegisterComponent},
  { path: 'login',component: LoginComponent},
  {path:'**',redirectTo: '/'}
];
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]

})
export class AppRoutingModule { }
