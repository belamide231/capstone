import { Component } from '@angular/core';
import { PieChartComponent } from '../../../../components/pie-chart/pie-chart.component';
import { BarChartComponent } from '../../../../components/bar-chart/bar-chart.component';
import { ZigzagChartComponent } from '../../../../components/zigzag-chart/zigzag-chart.component';
import { CircularChartComponent } from '../../../../components/circular-chart/circular-chart.component';

@Component({
    standalone: true,
    imports: [PieChartComponent, BarChartComponent, ZigzagChartComponent, CircularChartComponent],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.sass'
})
export class DashboardComponent {

}
