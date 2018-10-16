import { Component, OnInit } from '@angular/core';
import { AdalService } from '../services/adal.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss']
})
export class LogoutComponent implements OnInit {

   constructor(private service: AdalService) {

  }

  ngOnInit() {
    if (this.service.isLogged) {
    this.service.context.logOut();
    }
  }
}
