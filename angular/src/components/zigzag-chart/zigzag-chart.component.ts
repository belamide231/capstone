import { Component, AfterViewInit, Input } from '@angular/core';
import { Chart, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
	selector: 'zigzag-chart',
	standalone: true,
	templateUrl: './zigzag-chart.component.html',
	styleUrls: ['./zigzag-chart.component.sass']
})
export class ZigzagChartComponent implements AfterViewInit {
    color1 = "#E38A2B";
    color2 = "#384C6A";

    @Input() months: any = [
        {
            month: 'January',
            messages: 238 
        },
        { 
            month: 'Febuary',
            messages: 1876 
        },
        { 
            month: 'March',
            messages: 11358
        },
        { 
            month: 'April',
            messages: 93899
        },
        { 
            month:'May',
            messages: 503100 
        },
        { 
            month: 'June',
            messages: 2317469 
        },
        {
            month: 'July',
            messages: 13465897
        }
    ]


	chart!: Chart<'line', number[], string>;


	ngAfterViewInit(): void {
		this.createChart();
	}

	createChart() {
        const months = this.months.map((object: any) => object.month);
        const messages = this.months.map((object: any) => object.messages);
        const colors = this.months.map((_: any, index: number) => (index + 1) % 2 === 0 ? this.color1 : this.color2);


		const ctx = document.getElementById('zigzagChart') as HTMLCanvasElement;

		if (!ctx) {
			console.error('Canvas element not found');
			return;
		}

		this.chart = new Chart(ctx, {
			type: 'line',
			data: {
				labels: months,
				datasets: [{
					label: 'Monthly Data',
					data: messages,
					borderColor: '#E38A2B',
					backgroundColor: 'rgba(227, 138, 43, 0.2)',
					pointBackgroundColor: colors,
					pointBorderColor: '#384C6A',
					pointRadius: 5,
					pointHoverRadius: 7,
					fill: false,
					tension: 0
				}]
			},
			options: {
				responsive: true,
				maintainAspectRatio: false,
				plugins: {
					legend: {
						display: false
					},
					title: {
						display: true,
						text: 'Total messages generated by months',
						color: '#B4B4B4',
						font: {
							size: 20,
							family: 'sans-serif',
							weight: 'lighter'
						},
						padding: {
							top: 20,
							bottom: 50
						}
					}
				},
				scales: {
					y: {
						beginAtZero: true,
						ticks: {
							color: '#B4B4B4',
							font: {
								size: 14
							}
						}
					},
					x: {
						ticks: {
							color: '#B4B4B4',
							font: {
								size: 14
							}
						}
					}
				}
			}
		});
	}
}