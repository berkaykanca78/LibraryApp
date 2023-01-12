import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GlobalVariables } from 'src/app/shared/constants/global-variables';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  title = 'LibraryApp';
  loginForm : FormGroup;

  constructor(public authService: AuthService, private router: Router, private alertify: AlertifyService, private globalVariables: GlobalVariables, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.pattern(this.globalVariables.passwordRegex)]],

    });
  }

  login() {
    this.authService.login(this.loginForm.getRawValue()).subscribe(next => {
      this.alertify.success("Successfully logged in");
      this.router.navigate(['/books']);
    }, error => {
      this.alertify.error(error);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

  logout() {
    localStorage.removeItem("token");
    this.alertify.warning("Successfully logged out");
    this.router.navigate(['/home']);

  }

}
