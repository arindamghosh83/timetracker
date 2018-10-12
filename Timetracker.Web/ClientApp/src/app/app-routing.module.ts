import { NgModule } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { WeeklyEffortComponent } from './weekly-effort/weekly-effort.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Route[] = [
  { path: 'login', component: LoginComponent }
    ,
  {
    path: '',
    component: WeeklyEffortComponent,
    pathMatch: 'full',
    canActivate: [AuthGuard]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
