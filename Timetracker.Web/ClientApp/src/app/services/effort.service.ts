import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { of } from 'rxjs/observable/of';
import { catchError, share } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import appConstants from '../constants/constants';
import { IProject, IWeeklyEffort, IEffortUpsertRequest } from '../models/effort.model';
@Injectable()
export class EffortService {
  constructor(
    private httpClient: HttpClient
  ) { }

  getAllProjects(): Observable<IProject[]> {
    // var allProjects: IProject[];
    const projects$ = this.httpClient
      .get<IProject[]>(
        `${environment.endpoints.apiBaseUri}${
        appConstants.getProjectsEndPoint
        }`
      )
      .pipe(
        share()
      );
    return projects$;
  }

  getEfforts(personId: string): Observable<IWeeklyEffort[]> {
    const userEfforts$ = this.httpClient
      .get<IWeeklyEffort[]>(
        `${environment.endpoints.apiBaseUri}${
        appConstants.getActivitiesEndPoint
        }${personId}`
      )
      .pipe(
        share(),
        catchError((err: HttpErrorResponse) => {
          console.log(err);
          return of(<IWeeklyEffort[]>[{
            weekEndDate: "",
            weekStartDate: "",
            efforts: []
          }]);
        })
      );
    return userEfforts$;
  }

  saveEfforts(weeklyEffort: IWeeklyEffort, userName: string): Observable<any> {

    var requestEfforts: IEffortUpsertRequest[] = [];

    for (var i = 0; i < weeklyEffort.efforts.length; i++) {
      if (weeklyEffort.efforts[i].project.id > 0 && weeklyEffort.efforts[i].effortPercent && weeklyEffort.efforts[i].effortPercent > 0) {
        requestEfforts.push(<IEffortUpsertRequest>{
          effortPercent: weeklyEffort.efforts[i].effortPercent,
          isDeleted: weeklyEffort.efforts[i].isDeleted == true,
          projectId: weeklyEffort.efforts[i].project.id,
          id: weeklyEffort.efforts[i].id,
          createdBy: userName
        });
      }
    }

    const userEfforts$ = this.httpClient
      .post<any>(
        `${environment.endpoints.apiBaseUri}${
        appConstants.upsertActivitiesEndPoint
        }`,
        {
          weekStartDate: weeklyEffort.weekStartDate,
          weekEndDate: weeklyEffort.weekEndDate,
          efforts: requestEfforts
        },
        { observe: "response" }
      )
    return userEfforts$;
  }
}