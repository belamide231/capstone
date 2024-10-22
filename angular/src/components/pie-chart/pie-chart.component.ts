import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Chart, registerables } from 'chart.js';
import { Input } from '@angular/core';


Chart.register(...registerables);


@Component({
	selector: 'pie-chart',
	standalone: true,
	imports: [FormsModule],
	templateUrl: './pie-chart.component.html',
	styleUrls: ['./pie-chart.component.sass']
})
export class PieChartComponent {


	@Input() admin: number = 1;
	@Input() moderators: number = 23;
	@Input() users: number = 231;


	chart!: Chart<'pie', number[], string>;


	ngAfterViewInit(): void {
		this.createChart();
	}


	createChart() {
		const ctx = document.getElementById('users-pieChart') as HTMLCanvasElement;

		if (!ctx) {
			console.error('Canvas element not found');
			return;
		}

		this.chart = new Chart(ctx, {
			type: 'pie',
			data: {
				labels: ['Admin', 'Moderators', 'Users'],
				datasets: [{
					data: [this.admin, this.moderators, this.users],
					backgroundColor: [
						'#879BBB',
						'#E38A2B',
						'#384C6A',
					],
					borderWidth: 0
				}]
			},
			options: {
				responsive: true,
				plugins: {
					legend: {
						position: 'bottom',
						labels: {
							color: '#B4B4B4',
							font: {
								size: 15,
								family: 'sans-serif',
								weight: 'lighter'
							},
							padding: 20
						},
					},
					title: {
						display: true,
						text: 'Quantity comparison by roles',
						color: '#B4B4B4',
						font: {
							size: 20,
							family: 'sans-serif',
							weight: 'lighter',
						},
						padding: {
							top: 20,
							bottom: 20
						}
					},
				}
			}
		});
	}
}