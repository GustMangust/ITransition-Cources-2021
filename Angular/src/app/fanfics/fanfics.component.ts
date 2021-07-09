import { Component, OnInit } from '@angular/core';
import {ServerBounderService} from "../services/server-bounder.service";
import {Fanfic} from "../model/Fanfic";
import {Fandom} from "../model/Fandom";
import { FanficRatingFandomTemplate} from "../model/FanficRatingFandomTemplate";
import {Router} from "@angular/router";


@Component({
  selector: 'app-fanfics',
  templateUrl: './fanfics.component.html',
  styleUrls: ['./fanfics.component.css']
})
export class FanficsComponent implements OnInit {
  constructor(private serverBounder:ServerBounderService,private router:Router) { }
  cardInfo!: Array<Array<FanficRatingFandomTemplate>>;
  ngOnInit(): void {
    this.serverBounder.fanfics().subscribe(res=>{
      console.log(res);
      this.cardInfo = res;
    });
    this.router.navigateByUrl('/fanfics');
  }

}
