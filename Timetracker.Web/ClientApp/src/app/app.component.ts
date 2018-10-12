import { Component } from '@angular/core';
import { AdalService } from './services/adal.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  
  title = 'app';
  username: any = '';

  constructor(
    private service: AdalService
  ) {}
  ngOnInit() {
    this.service.context.handleWindowCallback();
    this.service.context.getUser((msg, user) => {
      if (user) {
        let { profile: { name = '', unique_name = '' } = {} } = user;
        name = name.slice(-1) === ',' ? name.slice(0, name.length - 1) : name; // Remove the ',' at the end of the name
        this.username = name;
      }
    });
  }
}
