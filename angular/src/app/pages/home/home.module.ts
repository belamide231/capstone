import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { CustomHeaderComponent } from '../../../components/custom-header/custom-header.component';


@NgModule({
	declarations: [
		HomeComponent
	],
	imports: [
		CommonModule,
		CustomHeaderComponent
	],
	exports: [
		HomeComponent
	]
})
export class HomeModule { }
