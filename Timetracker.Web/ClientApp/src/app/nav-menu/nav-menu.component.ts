import { Component, Input, OnInit } from '@angular/core';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {
  @Input() username;
  @Input() isAdmin: boolean;
  title: string;
  constructor() { }


  ngOnInit(): void {
     (this.title = "Effort Tracker");

  }
}
