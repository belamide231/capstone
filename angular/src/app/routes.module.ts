import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { RegisterComponent } from "./register/register.component";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./home/home.component";
import { authorizationGuard } from "../authorizations/authorization.guard";
import { RecoverComponent } from "./recover/recover.component";

const routes: Routes = [
    { path: "", component: HomeComponent, pathMatch: "full", canActivate: [authorizationGuard] },
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
