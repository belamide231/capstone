import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RoutesModule } from './routes.module';
import { RegisterModule } from './pages/register/register.module';
import { LoginModule } from './pages/login/login.module';
import { RecoverModule } from './pages/recover/recover.module';
import { HomeModule } from './pages/home/home.module';
import { DashboardModule } from './pages/dashboard/dashboard.module';


@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RoutesModule,
        RegisterModule,
        LoginModule,
        RecoverModule,
        HomeModule,
        DashboardModule
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
