import { Component, ElementRef, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from 'src/app/shared/models/user';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { UserService } from 'src/app/shared/services/user.service';
import { AddUserComponent } from './add-user/add-user.component';
import { DeleteUserComponent } from './delete-user/delete-user.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { BaseComponent } from 'src/app/shared/bases/base.component';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent extends BaseComponent implements OnInit {
  users = [];
  pageOfItems: Array<any>;
  constructor(private userService: UserService, private alertify: AlertifyService, private modalService: NgbModal) {
    super();
  }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    setTimeout(() => {
      this.userService.getUsersForDashboard().subscribe(result => {
        this.users = result.data.map(x => ({
          id: (x.id), city: (x.city), country: (x.country), createdDate: (x.createdDate),
          dateOfBirth: (x.dateOfBirth), gender: (x.gender), hobbies: (x.hobbies), imageUrl: (x.imageUrl), introduction: (x.introduction),
          lastActiveDate: (x.lastActiveDate),
          name: (x.name), surname: (x.surname), no: (x.no), userName: (x.userName), genderName: (x.genderName), phoneNumber: (x.phoneNumber),
          streetName: (x.streetName), zipCode: (x.zipCode), fullAddress: (x.fullAddress), email: (x.email), age: (x.age), roleName: (x.roleName)
        })) as User[];
      }, err => {
        this.alertify.error(err);
      })
    }, 1000)

  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }

  openAddUserModal() {
    this.modalService.open(AddUserComponent);
  }
  openUpdateUserModal(user: User) {
    const modalRef = this.modalService.open(UpdateUserComponent);
    modalRef.componentInstance.user = user;
  }
  openDeleteUserModal(userId: number) {
    const modalRef = this.modalService.open(DeleteUserComponent);
    modalRef.componentInstance.userId = userId;
  }
}
