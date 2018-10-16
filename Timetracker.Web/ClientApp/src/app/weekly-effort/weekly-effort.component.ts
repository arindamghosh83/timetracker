import { Component, Output, Input, OnInit } from '@angular/core';
import { IWeeklyEffort, IEffort, IProject } from '../models/effort.model';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { DataProvider } from '../data-provider'
import { EffortService } from '../services/effort.service'
import { AdalService } from '../services/adal.service';
@Component({
  selector: 'weekly-effort',
  styleUrls: ['./weekly-effort.component.scss'],
  templateUrl: './weekly-effort.component.html'
})
export class WeeklyEffortComponent implements OnInit {
  efforts: IWeeklyEffort[];
  currentEffort: IWeeklyEffort;
  effortWeekCounter: number;
  totalEffort: number;
  outgoingEffort: IEffort;
  projects: any;
  selectedProjects: any;
  displayStartDate: string;
  displayEndDate: string;
  username: string;
  dataLoaded: boolean;

  constructor(
    private toastr: ToastrService,
    private service: EffortService,
    private adalService: AdalService
  ) {
    this.currentEffort = <IWeeklyEffort>{ weekStartDate: '', weekEndDate: '', efforts: [] };
    this.dataLoaded = false;
  }
  ngOnInit(): void {

    this.getUserName();

    this.service.getAllProjects().subscribe(
      allProjectsReceived => {
        if (allProjectsReceived && allProjectsReceived.length > 0) {
          this.projects = allProjectsReceived;
          this.projects.push(<IProject>{
            description: '',
            id: 0,
            active: true
          });
          this.dataLoaded = true;
          this.service.getEfforts(this.username).subscribe(
            allEfforts => {
              if (allEfforts && this.projects && this.projects.length > 0) {

                this.efforts = allEfforts;

                this.assignSelectableProjects(this.efforts);

                this.effortWeekCounter = 0;
                this.currentEffort = this.efforts[this.effortWeekCounter];
                this.displayStartDate = this.getDisplayDate(this.currentEffort.weekStartDate);
                this.displayEndDate = this.getDisplayDate(this.currentEffort.weekEndDate);
                this.totalEffort = this.getTotalEffort(this.currentEffort.efforts);
              }
            },
            error => {
            }
          );
        }
      },
      error => {

      }
    );

  }

  getDisplayDate(date) {
    return moment(date).format('MM/DD/YYYY');
  }
  effortUpdate(outgoingEffort: IEffort) {
    this.totalEffort = this.getTotalEffort(this.currentEffort.efforts);
    if (!outgoingEffort.project.description || outgoingEffort.project.description.length == 0) {
      outgoingEffort.project.description = this.getProjectDescription(outgoingEffort.project.id);
    }
    this.updateSelectableProjects();
  }

  nextEffort() {
    if (this.efforts[this.effortWeekCounter - 1]) {
      this.effortWeekCounter -= 1
      this.currentEffort = this.efforts[this.effortWeekCounter];
      this.totalEffort = this.getTotalEffort(this.currentEffort.efforts);
      this.displayStartDate = this.getDisplayDate(this.currentEffort.weekStartDate);
      this.displayEndDate = this.getDisplayDate(this.currentEffort.weekEndDate);
    }
  }
  previousEffort() {
    if (this.efforts[this.effortWeekCounter + 1]) {
      this.effortWeekCounter += 1
      this.currentEffort = this.efforts[this.effortWeekCounter];
      this.totalEffort = this.getTotalEffort(this.currentEffort.efforts);
      this.displayStartDate = this.getDisplayDate(this.currentEffort.weekStartDate);
      this.displayEndDate = this.getDisplayDate(this.currentEffort.weekEndDate);
    }
  }
  addEffort() {
    var newEffort = (<IEffort>{
      id: 0,
      project: {
        description: "",
        id: 0
      },
      effortPercent: 0,
      isDeleted: false,
      selectableProjects: this.getSelectableProjects(this.projects, this.getSelectedProjectsForWeeklyEffort(this.currentEffort.efforts), <IProject>{
        description: "",
        id: 0
      })
    });
    this.currentEffort.efforts.push(newEffort);
  }

  saveEffort() {
    var response$ = this.service.saveEfforts(this.currentEffort, this.username);
    response$.subscribe(response => {
      if (response && (response.status == 200 || response.status == 201)) {
        this.toastr.success('Efforts Updated', 'Saved!');
      } else {
        this.toastr.error('Effort Update Failed. Please contact Support.', 'Unable to Save.');
      }
    }, error => {
      console.log(error);
      this.toastr.error('Effort Update Failed. Please contact Support.', 'Unable to Save.');
    });
  }

  getTotalEffort(effortList) {
    var effortTotal = 0;
    if (effortList) {
      for (var i = 0; i < effortList.length; i++) {
        if (!effortList[i].isDeleted && effortList[i].project && effortList[i].project.id != 0)
          effortTotal += effortList[i].effortPercent;
      }
    }
    return effortTotal;
  }

  getSelectableProjects(allProjects: IProject[], excludedProjectIds: IProject[], currentProjectId: IProject) {
    var foundSelectableProjects = [];
    if (allProjects) {
      for (var i = 0; i < allProjects.length; i++) {
        if ((excludedProjectIds.filter(obj => obj.id == allProjects[i].id).length > 0)) {
          if (currentProjectId.id == allProjects[i].id) {
            foundSelectableProjects.push(allProjects[i]);
          }
        } else {
          foundSelectableProjects.push(allProjects[i]);
        }
      }
    }
    return foundSelectableProjects;
  }

  getSelectedProjectsForWeeklyEffort(weeklyEffortProjects: IEffort[]) {
    var foundSelectableProjects = [];
    if (weeklyEffortProjects) {
      for (var i = 0; i < weeklyEffortProjects.length; i++) {
        if (!weeklyEffortProjects[i].isDeleted && weeklyEffortProjects[i].project.id != 0) {
          foundSelectableProjects.push(weeklyEffortProjects[i].project);
        }
      }
    }
    return foundSelectableProjects;
  }

  getProjectDescription(projectId: Number) {
    for (var projectIndex = 0; projectIndex < this.projects.length; projectIndex++) {
      if (this.projects[projectIndex].id == projectId) {
        return this.projects[projectIndex].description;
      }
    }
    return '';
  }

  updateSelectableProjects() {
    for (var i = 0; i < this.currentEffort.efforts.length; i++) {
      if (!this.currentEffort.efforts[i].isDeleted) {
        this.currentEffort.efforts[i].selectableProjects =
          this.getSelectableProjects(this.projects, this.getSelectedProjectsForWeeklyEffort(this.currentEffort.efforts), this.currentEffort.efforts[i].project);
      }
    }
  }

  addStartUpProjects() {
    if (this.efforts && this.efforts.length > 1) {
      if (this.efforts[0].efforts.length == 0) {
        for (var effortIndex = 0; effortIndex < this.efforts[1].efforts.length; effortIndex++) {

          this.efforts[0].efforts.push(<IEffort>{
            id: 0,
            project: {
              description: this.efforts[1].efforts[effortIndex].project.description,
              id: this.efforts[1].efforts[effortIndex].project.id
            },
            effortPercent: 0,
            isDeleted: false,
            selectableProjects: []
          });
        }
      }
    }
  }

  assignSelectableProjects(allEfforts: IWeeklyEffort[]) {
    var foundSelectableProjects = [];
    if (allEfforts) {
      for (var weekIndex = 0; weekIndex < allEfforts.length; weekIndex++) {

        for (var effortIndex = 0; effortIndex < allEfforts[weekIndex].efforts.length; effortIndex++) {

          allEfforts[weekIndex].efforts[effortIndex].selectableProjects =
            this.getSelectableProjects(this.projects, this.getSelectedProjectsForWeeklyEffort(allEfforts[weekIndex].efforts), allEfforts[weekIndex].efforts[effortIndex].project);
        }
      }
    }
    return foundSelectableProjects;
  }

  getUserName() {
    this.adalService.context.getUser((msg, user) => {
      if (user) {
        let { profile: { name = '', unique_name = '' } = {} } = user;
        if (unique_name) {
          var index: string = unique_name;
          this.username = index.split('@')[0];
        }
      }
    });
  }
}
