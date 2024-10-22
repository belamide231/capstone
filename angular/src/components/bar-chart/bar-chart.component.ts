





import { Component, AfterViewInit, Input } from '@angular/core';
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
    @Input() january: number = 10;
    @Input() february: number = 3;
    @Input() march: number = 15;
    @Input() april: number = 25;
    @Input() may: number = 30;
    @Input() june: number = 5;

    chart!: Chart<'bar', number[], string>;

    ngAfterViewInit(): void {
        this.createChart();
    }

    createChart() {
        const ctx = document.getElementById('usersGrowth-monthlyChart') as HTMLCanvasElement;

        if (!ctx) {
            console.error('Canvas element not found');
            return;
        }

        this.chart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June'],
                datasets: [{
                    label: 'Monthly Data',
                    data: [this.january, this.february, this.march, this.april, this.may, this.june],
                    backgroundColor: [
                        '#E38A2B',
                        '#384C6A',
                        '#879BBB',
                        '#E38A2B',
                        '#384C6A',
                        '#879BBB',
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
                                size: 16,
                                family: 'Arial, sans-serif',
                                weight: 'lighter'
                            },
                            padding: 10
                        },
                    },
                    title: {
                        display: true,
                        text: 'Monthly system growth',
                        color: '#B4B4B4',
                        font: {
                            size: 20,
                            family: 'sans-serif',
                            weight: 'lighter'
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
