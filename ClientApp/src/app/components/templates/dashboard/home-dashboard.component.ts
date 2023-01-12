import { Component, OnInit } from '@angular/core';
import { Count } from 'src/app/shared/models/count';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { DashboardService } from 'src/app/shared/services/dashboard.service';

@Component({
  selector: 'app-home-dashboard',
  templateUrl: './home-dashboard.component.html',
  styleUrls: ['./home-dashboard.component.css']
})
export class HomeDashboardComponent implements OnInit {
  public loading = false;
  counts: Count;
  constructor(private dashboardService: DashboardService, private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.loading = true;
    setTimeout(() => {
      this.loading = false;
      this.getCountsForDashboard();
    }, 1000);
  }

  getCountsForDashboard() {
    this.dashboardService.getCountsForDashboard().subscribe(result => {
      this.counts = result.data as Count;
    }, err => {
      this.alertify.error(err);
    });
  }
}
