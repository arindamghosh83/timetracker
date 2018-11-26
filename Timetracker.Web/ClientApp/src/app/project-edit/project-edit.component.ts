import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { EffortService } from "../services/effort.service";
import { IProject } from "../models/effort.model";
import { ActivatedRoute, Router } from "@angular/router";
import { AppInsightsService } from "../services/app-insights.service";

@Component({
  selector: "app-project-edit",
  templateUrl: "./project-edit.component.html",
  styleUrls: ["./project-edit.component.scss"]
})
export class ProjectEditComponent implements OnInit {
  project: IProject;
  projectToBeEdited: IProject = {
    description: "",
    active: false,
    funded: false,
    id: 0
  };
  projectId: number;
  @Input()
  isProjectVisible: boolean = true;
  @Output()
  isNavigationRequiredAfterProjectSaved = new EventEmitter<IProject>();
  constructor(
    private effortService: EffortService,
    private route: ActivatedRoute,
    private router: Router,
    private appInsightsService: AppInsightsService
  ) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.projectId = params.id;
      if (this.effortService.allProjects) {
        this.project = this.effortService.allProjects.find(
          p => p.id == this.projectId
        );
      }
      this.projectToBeEdited = { ...this.project };
    });
  }
  saveProject = () => {
    this.effortService
      .saveProject(this.projectToBeEdited)
      .subscribe(response => {
        if (this.projectId) {
          this.router.navigate(["/projects"]);
        } else {
          this.isNavigationRequiredAfterProjectSaved.emit(<IProject>{
            description: this.projectToBeEdited.description,
            active: this.projectToBeEdited.active,
            funded: this.projectToBeEdited.funded,
            id: response.id
          });
          this.resetProjectToBeEdited();
        }
      });
  };
  undo = () => {
    if (this.projectId) {
      this.router.navigate(["/projects"]);
    } else {
      this.isNavigationRequiredAfterProjectSaved.emit(null);
    }
  };
  resetProjectToBeEdited = () => {
    this.projectToBeEdited = {
      description: "",
      active: false,
      funded: false,
      id: 0
    };
  };
}
