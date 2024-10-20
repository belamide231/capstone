import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RegisterModule } from './register/register.module';
import { RoutesModule } from './routes.module';
import { LoginModule } from './login/login.module';
import { HomeModule } from './home/home.module';
import { RecoverModule } from './recover/recover.module';



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
        HomeModule
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
