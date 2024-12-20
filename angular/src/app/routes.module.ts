import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './pages/pre-logged/register/register.component';
import { LoginComponent } from './pages/pre-logged/login/login.component';
import { RecoverComponent } from './pages/pre-logged/recover/recover.component';
import { AuthorizationGuard } from '../authorizations/authorization.guard';
import { PostLoggedComponent } from './pages/post-logged/post-logged.component';
import { CustomTableComponent } from '../components/custom-table/custom-table.component';
import { DashboardComponent } from './pages/post-logged/dashboard/dashboard.component';
import { HomeComponent } from './pages/post-logged/home/home.component';
import { PopularComponent } from './pages/post-logged/popular/popular.component';
import { QuestionsComponent } from './pages/post-logged/questions/questions.component';
import { ExploreComponent } from './pages/post-logged/explore/explore.component';
import { DepartmentsComponent } from './pages/post-logged/departments/departments.component';
import { RequestComponent } from './pages/post-logged/request/request.component';
import { DepartmentComponent } from './pages/post-logged/department/department.component';
import { PendingComponent } from './pages/post-logged/pending/pending.component';

const routes: Routes = [
    { 
        path: '',
        component: PostLoggedComponent,
        canActivate: [AuthorizationGuard],
        children: [
            { path: 'users', component: CustomTableComponent },
            { path: 'dashboard', component: DashboardComponent }, 
            { path: 'popular', component: PopularComponent },
            { path: 'questions', component: QuestionsComponent},
            { path: 'explore', component: ExploreComponent },
            { path: 'departments', component: DepartmentsComponent },
            { path: 'department/:departmentName', component: DepartmentComponent },
            { path: 'request', component: RequestComponent },
            { path: 'pending', component: PendingComponent },
            { path: '', component: HomeComponent }
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
