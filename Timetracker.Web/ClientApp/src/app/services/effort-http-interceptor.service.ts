import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpSentEvent,
  HttpHeaderResponse,
  HttpProgressEvent,
  HttpResponse,
  HttpUserEvent
} from '@angular/common/http';
import { AdalService } from './adal.service';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/mergeMap';

@Injectable()
export class EffortHttpInterceptorService implements HttpInterceptor {
  private token: any;

  constructor(private adalService: AdalService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<
    | HttpSentEvent
    | HttpHeaderResponse
    | HttpProgressEvent
    | HttpResponse<any>
    | HttpUserEvent<any>
  > {
    return this.adalService
      .acquireToken(environment.adalConfig.apiId)
      .mergeMap(token => {
        if (token) {
          req = req.clone({
            headers: req.headers
              .set('Authorization', 'Bearer ' + token)
              .set('Ocp-Apim-Subscription-Key', environment.endpoints.subscriptionKey)
              .set('Cache-Control', 'no-cache')
              .set('Pragma', 'no-cache, no-store')
          });
        }
        return next.handle(req);
      });
  }
}
