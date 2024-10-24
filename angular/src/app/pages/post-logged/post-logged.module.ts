import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostLoggedComponent } from './post-logged.component';
import { CustomHeaderComponent } from '../../../components/custom-header/custom-header.component';
import { CustomAsideComponent } from '../../../components/custom-aside/custom-aside.component';
import { PieChartComponent } from '../../../components/pie-chart/pie-chart.component';
import { RoutesModule } from '../../routes.module';
import { CustomTableComponent } from '../../../components/custom-table/custom-table.component';



@NgModule({
	declarations: [
		PostLoggedComponent
	],
	imports: [
		CommonModule,
		CustomHeaderComponent,
		CustomAsideComponent,
		PieChartComponent,
		RoutesModule,
		CustomTableComponent
	],
	exports: [
		PostLoggedComponent
	]
})
export class PostLoggedModule { } 

