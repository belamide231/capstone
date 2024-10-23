import { Component, AfterViewInit, Input } from '@angular/core';
import { Chart, registerables } from 'chart.js';


Chart.register(...registerables);

@Component({
    selector: 'circular-chart',
    standalone: true,
    templateUrl: './circular-chart.component.html',
    styleUrls: ['./circular-chart.component.sass']
})
export class CircularChartComponent implements AfterViewInit {


    @Input() totalUsers: number = 888;
    @Input() totalActives: number = 538;


    chart!: Chart<'doughnut', number[], string>;

    ngAfterViewInit(): void {
        this.createChart();
    }

    createChart() {
        const ctx = document.getElementById('circularChart') as HTMLCanvasElement;
        const activePercentage = (this.totalActives / this.totalUsers) * 100;

        if (!ctx) {
            console.error('Canvas element not found');
            return;
        }

        if (this.chart) {
            this.chart.destroy();
        }

        this.chart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['Online', 'Offline'],
                datasets: [{
                    label: 'Sample Data',
                    data: [this.totalActives, this.totalUsers - this.totalActives], 
                    backgroundColor: ['#E38A2B', '#384C6A'],
                    hoverBackgroundColor: ['#E38A2B', '#384C6A'],
                    borderWidth: 0
                }],
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '80%', 
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            color: '#B4B4B4',
                            font: {
                                family: 'sans-serif', 
                                size: 16,
                                weight: 'lighter'
                            },
                            padding: 20
                        }
                    },
                    title: {
                            display: true,
                            text: 'Current online users',
                            color: '#B4B4B4',
                            font: {
                            family: 'sans-serif',
                            size: 20,
                            weight: 'lighter'
                        },
                        padding: 20
                    }
                }
            },
            plugins: [{
                id: 'center-text',
                afterDraw: (chart) => {
                    const { ctx, chartArea } = chart;
                    const centerX = (chartArea.left + chartArea.right) / 2;
                    const centerY = (chartArea.top + chartArea.bottom) / 2;

                    ctx.save();
                    ctx.font = 'lighter 24px sans-serif';
                    ctx.fillStyle = '#B4B4B4';
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'middle';
                    ctx.fillText(`${activePercentage.toFixed(2)}% Online`, centerX, centerY);
                    ctx.restore();
                }
            }]
        });
    }
}
