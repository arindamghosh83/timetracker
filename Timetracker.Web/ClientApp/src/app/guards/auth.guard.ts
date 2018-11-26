import { Injectable } from "@angular/core";
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from "@angular/router";
import { Observable } from "rxjs/Observable";
import { adal } from "adal-angular";
import { environment } from "../../environments/environment";
import { AdalService } from "../services/adal.service";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private service: AdalService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    if (this.service.isLogged) {
      return true;
    } else {
      this.router.navigate(["login"], {
        queryParams: { returnUrl: state.url }
      });
    }
  }
}
