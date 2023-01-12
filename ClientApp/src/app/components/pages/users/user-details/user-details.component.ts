import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent extends BaseComponent implements OnInit {
  user:User;
  constructor(private route: ActivatedRoute) {
    super();
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.user.data;
    })
  }

}
