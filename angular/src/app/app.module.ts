import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RegisterModule } from './register/register.module';
import { RoutesModule } from './routes.module';



@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    FormsModule,
    RoutesModule,
    RegisterModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
