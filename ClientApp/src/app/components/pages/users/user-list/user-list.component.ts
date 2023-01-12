import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { User } from 'src/app/shared/models/user';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
  public loading = false;
  userParams: any = {};
  form: FormGroup;
  pageOfItems: Array<any>;

  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.userParams.orderBy = "lastActive";
    this.userParams.ordering = "desc";
    this.getUsers();
  }

  getUsers() {
    this.loading = true;
    setTimeout(() => {
      this.loading = false;
      this.userService.getUsers(this.userParams).subscribe(result => {
        this.users = result.data.map(x => ({
          id: (x.id), city: (x.city), country: (x.country), createdDate: (x.createdDate),
          dateOfBirth: (x.dateOfBirth), gender: (x.gender), hobbies: (x.hobbies), imageUrl: (x.imageUrl), introduction: (x.introduction),
          lastActiveDate: (x.lastActiveDate),
          name: (x.name), surname: (x.surname)
        })) as User[];
      }, err => {
        this.alertify.error(err);
      })
    }, 1000);
  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }
}
