import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './pages/pre-logged/register/register.component';
import { LoginComponent } from './pages/pre-logged/login/login.component';
import { RecoverComponent } from './pages/pre-logged/recover/recover.component';
import { AuthorizationGuard } from '../authorizations/authorization.guard';
import { PostLoggedComponent } from './pages/post-logged/post-logged.component';
import { CustomTableComponent } from '../components/custom-table/custom-table.component';
import { DashboardComponent } from './pages/post-logged/dashboard/dashboard.component';

const routes: Routes = [
    { 
        path: '',
        component: PostLoggedComponent,
        canActivate: [AuthorizationGuard],
        children: [
            { path: 'users', component: CustomTableComponent },
            { path: 'dashboard', component: DashboardComponent }
        ]
    },
    { path: 'register', component: RegisterComponent, canActivate: [AuthorizationGuard] },
    { path: 'login', component: LoginComponent, canActivate: [AuthorizationGuard] },
    { path: 'recover', component: RecoverComponent, canActivate: [AuthorizationGuard] },
    { path: '**', redirectTo: '/login' },
];


@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class RoutesModule {}
