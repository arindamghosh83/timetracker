import { Injectable, OnInit } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute } from '@angular/router';

import { of } from 'rxjs/observable/of';
import { tap, catchError, map, delay, share } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/Subject';
import { environment } from '../../environments/environment';
import { combineLatest } from 'rxjs/observable/combineLatest';
import { Subscription } from 'rxjs/Subscription';
import appConstants from '../constants/constants';
import { IProject, IWeeklyEffort } from '../models/effort.model';
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
                weekEndDate:"",
                weekStartDate: "",
                efforts: []
              }]);
            })
          );
        return userEfforts$;
      }

      saveEfforts(weeklyEffort: IWeeklyEffort): boolean {
        const userEfforts$ = this.httpClient
          .post<any>(
            `${environment.endpoints.apiBaseUri}${
              appConstants.upsertActivitiesEndPoint
            }`,
            {
              weekStartDate: weeklyEffort.weekStartDate,
              weekEndDate: weeklyEffort.weekEndDate,
              efforts: weeklyEffort.efforts
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