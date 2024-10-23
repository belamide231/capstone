





import { Component, AfterViewInit, Input, numberAttribute } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Chart, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
    selector: 'bar-chart',
    standalone: true,
    imports: [FormsModule],
    templateUrl: './bar-chart.component.html',
    styleUrls: ['./bar-chart.component.sass']
})
export class BarChartComponent implements AfterViewInit {
    color1 = "#E38A2B";
    color2 = "#384C6A";

    @Input() months: any = [
        {
            month: 'January',
            users: 238 
        },
        { 
            month: 'Febuary',
            users: 182 
        },
        { 
            month: 'March',
            users: 632
        },
        { 
            month: 'April',
            users: 742 
        },
        { 
            month:'May',
            users: 231 
        },
        { 
            month: 'June',
            users: 68 
        },
        {
            month: 'July',
            users: 1923
        }
    ]


    chart!: Chart<'bar', number[], string>;
    

    ngAfterViewInit(): void {
        this.createChart();
    }

    createChart() {

        const months = this.months.map((object: any) => object.month);
        const users = this.months.map((object: any) => object.users);
        const colors = this.months.map((_: any, index: number) => (index + 1) % 2 === 0 ? this.color1 : this.color2);

        const ctx = document.getElementById('usersGrowth-monthlyChart') as HTMLCanvasElement;

        if (!ctx) {
            console.error('Canvas element not found');
            return;
        }

        this.chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: months,
                datasets: [{
                    data: users,
                    backgroundColor: colors,
                    borderWidth: 0
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: false
                    },
                    title: {
                        display: true,
                        text: 'Users quantity monthly comparison',
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
                                size: 14,
                                family: 'sans-serif',
                                weight: 'lighter'
                            }
                        }
                    },
                    x: {
                        ticks: {
                            color: '#B4B4B4',
                            font: {
                                size: 14,
                                family: 'sans-serif',
                                weight: 'lighter'
                            }
                        }
                    }
                }
            }
        });
    }
}
