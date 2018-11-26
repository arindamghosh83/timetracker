import { Injectable } from "@angular/core";
import { Router, ResolveEnd, ActivatedRouteSnapshot } from "@angular/router";
import { AppInsights } from "applicationinsights-js";
import { environment } from "../../environments/environment";

@Injectable()
export class AppInsightsService {
  private config: Microsoft.ApplicationInsights.IConfig = {
    instrumentationKey: environment.appInsights.instrumentationKey
  };
  constructor(private router: Router) {
    if (!AppInsights.config) {
      AppInsights.downloadAndSetup(this.config);
    }
    router.events.subscribe(event => {
      if (event instanceof ResolveEnd) {
        const activatedComponent = this.getActivatedComponent(event.state.root);
        if (activatedComponent) {
          this.logPageView(
            `${activatedComponent.name} ${this.getRouteTemplate(
              event.state.root
            )}`,
            event.urlAfterRedirects
          );
        }
      }
    });
  }
  private getActivatedComponent(snapshot: ActivatedRouteSnapshot): any {
    if (snapshot.firstChild) {
      return this.getActivatedComponent(snapshot.firstChild);
    }

    return snapshot.component;
  }
  private getRouteTemplate(snapshot: ActivatedRouteSnapshot): string {
    let path = "";
    if (snapshot.routeConfig) {
      path += snapshot.routeConfig.path;
    }

    if (snapshot.firstChild) {
      return path + this.getRouteTemplate(snapshot.firstChild);
    }

    return path;
  }
  logPageView(
    name: string,
    url?: string,
    properties?: any,
    measurements?: any,
    duration?: number
  ) {
    AppInsights.trackPageView(name, url, properties, measurements, duration);
  }
  logEventView(name: string, properties?: any, measurements?: any) {
    AppInsights.trackEvent(name, properties, measurements);
  }
  logException(
    exception: Error,
    handledAt?: string,
    properties?: { [key: string]: string },
    measurements?: { [key: string]: number },
    severityLevel?: AI.SeverityLevel
  ) {
    AppInsights.trackException(
      exception,
      handledAt,
      properties,
      measurements,
      severityLevel
    );
  }
  logTrace(
    message: string,
    properties?: { [key: string]: string },
    severityLevel?: AI.SeverityLevel
  ) {
    AppInsights.trackTrace(message, properties, severityLevel);
  }
  setAuthenticatedUserId(userId: string): void {
    AppInsights.setAuthenticatedUserContext(userId);
  }
}
