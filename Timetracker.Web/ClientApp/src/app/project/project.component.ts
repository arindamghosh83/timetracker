import { Component, OnInit, Input } from "@angular/core";
import { IProject } from "../models/effort.model";
import { Router } from "@angular/router";

@Component({
  selector: "app-project",
  templateUrl: "./project.component.html",
  styleUrls: ["./project.component.scss"]
})
export class ProjectComponent implements OnInit {
  @Input()
  project: IProject;
  @Input()
  isAdmin: boolean;
  constructor(private router: Router) {}

  ngOnInit() {}
  editProject = () => this.router.navigate(["/projects", this.project.id]);
}
