import {Component, OnInit} from '@angular/core';
import {ServerBounderService} from "./services/server-bounder.service";
import {AccountService} from "./services/account.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit{
  title = 'Angular';
  constructor(private serverBounder:ServerBounderService,private accountService:AccountService) {

  }

  ngOnInit(): void {

  }

}
