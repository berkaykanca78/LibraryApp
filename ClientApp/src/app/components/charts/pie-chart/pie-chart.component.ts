import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import Chart from 'chart.js'
import { DashboardService } from 'src/app/shared/services/dashboard.service';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css'],
})
export class PieChartComponent implements AfterViewInit {
  canvas: any;
  ctx: any;
  @ViewChild('pieCanvas') pieCanvas!: { nativeElement: any };
  pieChart: any;
  categoriesLabelList: string[] = [];
  categoriesColorHexList: string[] = [];
  categoriesCountList: number[] = [];

  constructor(private dashboardService: DashboardService) { }


  ngAfterViewInit(): void {
    this.pieChartBrowser();
  }

  pieChartBrowser(): void {
    this.canvas = this.pieCanvas.nativeElement;
    this.ctx = this.canvas.getContext('2d');
    this.dashboardService.getCategoriesForBooksInfo().subscribe(categories => {
      categories.data.forEach(category => {
        this.categoriesColorHexList.push(category.hexCode);
        this.categoriesLabelList.push(category.label);
        this.categoriesCountList.push(category.count);
      });
      this.pieChart = new Chart(this.ctx, {
        type: 'pie',
        data: {
          labels: this.categoriesLabelList,
          datasets: [
            {
              backgroundColor: this.categoriesColorHexList,
              data: this.categoriesCountList,
            },
          ],
        },
      });
    });
  }
}

