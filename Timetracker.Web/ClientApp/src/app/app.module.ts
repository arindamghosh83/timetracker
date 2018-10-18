import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { WeeklyEffortComponent } from './weekly-effort/weekly-effort.component';
import { EfforRowComponent } from './effort-row/effort-row.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AdalService } from './services/adal.service';
import { EffortService } from './services/effort.service';
import { EffortHttpInterceptorService } from './services/effort-http-interceptor.service';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { LogoutComponent } from './logout/logout.component';
import { NumberOnlyDirective } from './directives/number-only.directive';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LogoutComponent,
    NavMenuComponent,
    WeeklyEffortComponent,
    CounterComponent,
    FetchDataComponent,
    EfforRowComponent,
    NumberOnlyDirective
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    AppRoutingModule
    
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: EffortHttpInterceptorService,
    multi: true
  }, AdalService, AuthGuard, EffortService],
  bootstrap: [AppComponent]
  
})
export class AppModule { }
