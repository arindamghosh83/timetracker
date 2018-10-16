import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse} from '@angular/common/http';
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
      ) {}

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

      getEfforts(): Observable<IWeeklyEffort[]> {
        const userEfforts$ = this.httpClient
          .get<IWeeklyEffort[]>(
            `${environment.endpoints.apiBaseUri}${
              appConstants.getActivitiesEndPoint
            }`
          )
          .pipe(
            share(),
            catchError((err: HttpErrorResponse) => {
              console.log(err);
              return of(<IWeeklyEffort[]>[{
                weekEndDate:"",
                weekStartDate: "",
                efforts: []
              }]);
            })
          );
        return userEfforts$;
      }

      saveEfforts(weeklyEffort: IWeeklyEffort): boolean {
        
        var requestEfforts: IEffortUpsertRequest[] = [];

        for(var i = 0; i < weeklyEffort.efforts.length; i++){
          requestEfforts.push(<IEffortUpsertRequest>{
            effortPercent: weeklyEffort.efforts[i].effortPercent,
            isDeleted: weeklyEffort.efforts[i].isDeleted == true,
            projectId: weeklyEffort.efforts[i].project.id,
            id: weeklyEffort.efforts[i].id
          });
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
            }
          )
          .pipe(
            share(),
            catchError((err: HttpErrorResponse) => {
              console.log(err);
              return of(false);
            })
          );
          userEfforts$.subscribe(response => {
            if (!response) {
              return false;
            }
          }); 
          return true;
      }
}