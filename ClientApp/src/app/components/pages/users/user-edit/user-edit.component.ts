import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { Token } from 'src/app/shared/models/token';
import { User } from 'src/app/shared/models/user';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent extends BaseComponent implements OnInit {
  token: Token;
  user: User;

  constructor(private route: ActivatedRoute,
    private userService: UserService,
    private alertify: AlertifyService,
    private authService: AuthService) {
    super();
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.user.data;
      this.authService.decodedToken.imageUrl = this.user.imageUrl;
      this.authService.decodedToken.userName = this.user.userName;
    });
  }

  updateUser() {
    this.userService.updateUser(this.user).subscribe(() => {
      this.alertify.success("You have successfully updated.");
    }, err => {
      this.alertify.error(err);
    });
  }

}
