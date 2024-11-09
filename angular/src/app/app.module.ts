import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RoutesModule } from './routes.module';
import { RegisterModule } from './pages/pre-logged/register/register.module';
import { LoginModule } from './pages/pre-logged/login/login.module';
import { RecoverModule } from './pages/pre-logged/recover/recover.module';
import { PostLoggedModule } from './pages/post-logged/post-logged.module';
import { HomeComponent } from './pages/post-logged/home/home.component';


@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RoutesModule,
        RegisterModule,
        LoginModule,
        RecoverModule,
        PostLoggedModule,
        HomeComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
