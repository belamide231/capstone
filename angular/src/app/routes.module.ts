import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./pages/home/home.component";
import { RegisterComponent } from "./pages/register/register.component";
import { LoginComponent } from "./pages/login/login.component";
import { RecoverComponent } from "./pages/recover/recover.component";
import { authorizationGuard } from "../authorizations/authorization.guard";
import { DashboardComponent } from "./pages/dashboard/dashboard.component";
import { UsersComponent } from "./pages/users/users.component";


const routes: Routes = [
    { path: "", component: HomeComponent, pathMatch: "full", canActivate: [authorizationGuard] },
    { path: "users", component: UsersComponent, canActivate: [authorizationGuard]},
    { path: "dashboard", component: DashboardComponent, canActivate: [authorizationGuard] },
    { path: "register", component: RegisterComponent, canActivate: [authorizationGuard] },
    { path: "login", component: LoginComponent, canActivate: [authorizationGuard] },
    { path: "recover", component: RecoverComponent, canActivate: [authorizationGuard] },
    { path: "**", redirectTo: "/login" },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class RoutesModule {}
