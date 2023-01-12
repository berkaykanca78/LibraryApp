import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Chart } from 'chart.js';
import { DashboardService } from 'src/app/shared/services/dashboard.service';

@Component({
  selector: 'app-doughnut-chart',
  templateUrl: './doughnut-chart.component.html',
  styleUrls: ['./doughnut-chart.component.css'],
})
export class DoughnutChartComponent implements AfterViewInit {
  @ViewChild('doughnutCanvas') doughnutCanvas: ElementRef | undefined;
  doughnutChart: any;
  citiesLabelList: string[] = [];
  citiesColorHexList: string[] = [];
  citiesCountList: number[] = [];
  citiesHoverHexColorList: string[] = [];
  constructor(private dashboardService: DashboardService) { }

  ngAfterViewInit() {
    this.doughnutChartMethod();
  }

  doughnutChartMethod() {
    this.dashboardService.getCitiesForUsersInfo().subscribe(cities => {
      cities.data.forEach(city => {
        this.citiesColorHexList.push(city.hexCode);
        this.citiesLabelList.push(city.label);
        this.citiesCountList.push(city.count);
        this.citiesHoverHexColorList.push(city.hoverHexCode);
      });
      this.doughnutChart = new Chart(this.doughnutCanvas?.nativeElement, {
        type: 'doughnut',
        data: {
          labels: this.citiesLabelList,
          datasets: [
            {
              label: 'Kullanıcı Sayıları',
              data: this.citiesCountList,
              backgroundColor: this.citiesColorHexList,
              hoverBackgroundColor: this.citiesHoverHexColorList,
            },
          ],
        },
      });
    });
  }
}
