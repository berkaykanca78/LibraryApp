import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import Chart from 'chart.js';
import { DashboardService } from 'src/app/shared/services/dashboard.service';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css'],
})
export class BarChartComponent implements AfterViewInit {
  @ViewChild('barCanvas') barCanvas: ElementRef | undefined;
  barChart: any;
  booksLabelList: string[] = [];
  booksColorHexList: string[] = [];
  booksCountList: number[] = [];
  booksBorderHexColorList: string[] = [];

  constructor(private dashboardService: DashboardService) { }

  ngAfterViewInit(): void {
    this.barChartMethod();
  }

  barChartMethod() {
    this.dashboardService.getBooksForAuthorsInfo().subscribe(books => {
      books.data.forEach(book => {
        this.booksColorHexList.push(book.hexCode);
        this.booksLabelList.push(book.label);
        this.booksCountList.push(book.count);
        this.booksBorderHexColorList.push(book.borderHexCode);
      });
      this.barChart = new Chart(this.barCanvas?.nativeElement, {
        type: 'bar',
        data: {
          labels: this.booksLabelList,
          datasets: [
            {
              label: 'Kitap Sayıları',
              data: this.booksCountList,
              backgroundColor: this.booksColorHexList,
              borderColor: this.booksBorderHexColorList,
              borderWidth: 1,
            },
          ],
        },
        options: {
          scales: {
            y: {
              beginAtZero: true,
            },
          },
        },
      });
    });


  }
}
