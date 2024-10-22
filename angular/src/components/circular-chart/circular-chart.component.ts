import { Component, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';

// Register Chart.js components
Chart.register(...registerables);

@Component({
  selector: 'circular-chart',
  standalone: true,
  templateUrl: './circular-chart.component.html',
  styleUrls: ['./circular-chart.component.sass']
})
export class CircularChartComponent implements AfterViewInit {
  chart!: Chart<'doughnut', number[], string>;

  ngAfterViewInit(): void {
    this.createChart();
  }

  createChart() {
    const ctx = document.getElementById('circularChart') as HTMLCanvasElement;

    if (!ctx) {
      console.error('Canvas element not found');
      return;
    }

    // Destroy existing chart if it exists
    if (this.chart) {
      this.chart.destroy();
    }

    this.chart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Category 1', 'Category 2'],
        datasets: [{
          label: 'Sample Data',
          data: [50, 50], // Data showing 50%
          backgroundColor: ['#E38A2B', '#384C6A'],
          hoverBackgroundColor: ['#E38A2B', '#384C6A'],
          borderWidth: 0 // Removes the border
        }],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        cutout: '80%', // Makes the donut thinner
        plugins: {
          legend: {
            position: 'bottom',
            labels: {
              color: '#B4B4B4',
              font: {
                family: 'sans-serif', // Set font family to sans-serif
                size: 16,
                weight: 'lighter' // Set font weight to lighter
              },
              padding: 20
            }
          },
          title: {
            display: true,
            text: 'Circular Chart',
            color: '#B4B4B4',
            font: {
              family: 'sans-serif', // Set font family to sans-serif
              size: 20,
              weight: 'lighter' // Set font weight to lighter
            },
            padding: 20
          }
        }
      },
      plugins: [{
        id: 'center-text', // Give your plugin an ID
        afterDraw: (chart) => {
          const { ctx, chartArea } = chart;
          const centerX = (chartArea.left + chartArea.right) / 2;
          const centerY = (chartArea.top + chartArea.bottom) / 2;

          // Draw the text
          ctx.save();
          ctx.font = 'lighter 24px sans-serif'; // Set the center text font
          ctx.fillStyle = '#B4B4B4';
          ctx.textAlign = 'center';
          ctx.textBaseline = 'middle';
          ctx.fillText('50%', centerX, centerY);
          ctx.restore();
        }
      }]
    });
  }
}
