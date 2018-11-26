import { Component, OnInit } from "@angular/core";
import { EffortService } from "../services/effort.service";
import { IProject } from "../models/effort.model";
import { ToastrService } from "ngx-toastr";
import { AppInsightsService } from "../services/app-insights.service";
import { ActivatedRoute } from "@angular/router";
@Component({
  selector: "app-projects-list",
  templateUrl: "./projects-list.component.html",
  styleUrls: ["./projects-list.component.scss"]
})
export class ProjectsListComponent implements OnInit {
  projects: IProject[];
  isAdmin: boolean = false;
  projectToBeEdited: IProject = {
    description: "",
    active: false,
    funded: false,
    id: 0
  };
  get allProjects(): IProject[] {
    return this.effortService.allProjects;
  }
  set allProjects(value) {
    this.effortService.allProjects = value;
  }
  currentPageProjects: IProject[];
  page: number;
  projectCount = 0;
  pageSize = 10;
  addProjectFlag: boolean = false;
  constructor(
    public effortService: EffortService,
    private toastr: ToastrService,
    private appInsightsService: AppInsightsService,
    private route: ActivatedRoute
  ) {
    this.isAdmin = this.effortService.isAdmin;
  }

  ngOnInit() {
    this.effortService.getAllProjects().subscribe(projects => {
      if (projects) {
        this.projects = projects;
        this.effortService.allProjects = projects;
        this.page = 1;
        this.projectCount = this.projects.length;
        this.pageChanged(this.page);
      }
    });
  }
  pageChanged(currentPage) {
    if (this.projects) {
      this.currentPageProjects = this.projects.filter((p, index) => {
        return (
          index >= (currentPage - 1) * this.pageSize &&
          index < currentPage * this.pageSize
        );
      });
    }
  }
  getSelectedProjectId = ($projectDescription: string) => {
    if ($projectDescription === "") {
      this.projects = [...this.allProjects];
    } else {
      this.projects = [
        ...this.allProjects.filter(p => {
          if (p.description) {
            return (
              p.description
                .toLowerCase()
                .indexOf($projectDescription.toLowerCase().trim()) !== -1
            );
          } else {
            return -1;
          }
        })
      ];
    }
    if (this.projects) {
      this.page = 1;
      this.projectCount = this.projects.length;
      this.pageChanged(this.page);
    }
  };
  addProject = () => {
    this.addProjectFlag = true;
  };
  undo = () => {
    this.addProjectFlag = false;
  };
  showNewlySavedProject = (savedProject: IProject) => {
    this.addProjectFlag = false;
    if (savedProject) {
      this.allProjects = [savedProject, ...this.allProjects];
      this.projects = [savedProject, ...this.projects];
      this.currentPageProjects = [savedProject, ...this.currentPageProjects];
      this.toastr.success("Project Added", "Saved!");
    }
  };
}
