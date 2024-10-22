import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { CustomHeaderComponent } from '../../../components/custom-header/custom-header.component';
import { PieChartComponent } from '../../../components/pie-chart/pie-chart.component';
import { ZigzagChartComponent } from '../../../components/zigzag-chart/zigzag-chart.component';
import { BarChartComponent } from "../../../components/bar-chart/bar-chart.component";
import { CircularChartComponent } from "../../../components/circular-chart/circular-chart.component";


@NgModule({
	declarations: [
		DashboardComponent
	],
	imports: [
    CommonModule,
    CustomHeaderComponent,
    PieChartComponent,
    ZigzagChartComponent,
    BarChartComponent,
    CircularChartComponent
],
	exports: [
		DashboardComponent
	]
})
export class DashboardModule { }
