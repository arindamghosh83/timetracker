import { NgModule } from "@angular/core";
import { Routes, RouterModule, Route } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { WeeklyEffortComponent } from "./weekly-effort/weekly-effort.component";
import { AuthGuard } from "./guards/auth.guard";
import { LogoutComponent } from "./logout/logout.component";
import { ProjectsListComponent } from "./projects-list/projects-list.component";
import { ProjectEditComponent } from "./project-edit/project-edit.component";
import { AdminGuard } from "./guards/admin.guard";

export const routes: Route[] = [
  { path: "login", component: LoginComponent },
  { path: "logout", component: LogoutComponent },
  {
    path: "projects/:id",
    component: ProjectEditComponent,
    canActivate: [AuthGuard, AdminGuard]
  },
  {
    path: "projects",
    component: ProjectsListComponent,
    pathMatch: "full",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard, AdminGuard]
  },
  {
    path: "",
    component: WeeklyEffortComponent,
    pathMatch: "full",
    canActivate: [AuthGuard]
  },
  { path: "**", redirectTo: "", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: "reload" })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
