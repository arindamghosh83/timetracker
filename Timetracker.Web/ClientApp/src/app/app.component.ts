import { Component } from "@angular/core";
import { AdalService } from "./services/adal.service";
import { EffortService } from "./services/effort.service";
import { Router, ActivatedRoute } from "@angular/router";
@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  title = "app";
  username: any = "";
  isAdmin: boolean = false;
  constructor(
    private service: AdalService,
    private effortService: EffortService
  ) {}
  ngOnInit() {
    this.service.context.handleWindowCallback();
    this.service.context.getUser((msg, user) => {
      if (user) {
        let {
          profile: { name = "", unique_name = "", roles = [] } = {}
        } = user;
        name = name.slice(-1) === "," ? name.slice(0, name.length - 1) : name; // Remove the ',' at the end of the name
        this.username = name;
        if (roles && roles[0] === "Admin") {
          this.isAdmin = true;
          this.effortService.isAdmin = true;
        }
      }
    });
  }
}
