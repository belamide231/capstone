import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { CustomHeaderComponent } from '../../../components/custom-header/custom-header.component';
import { CustomAsideComponent } from "../../../components/custom-aside/custom-aside.component";


@NgModule({
	declarations: [
		HomeComponent
	],
	imports: [
    CommonModule,
    CustomHeaderComponent,
    CustomAsideComponent
],
	exports: [
		HomeComponent
	]
})
export class HomeModule { }
