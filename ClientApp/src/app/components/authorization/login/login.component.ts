import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent implements OnInit {
  loginForm: FormGroup;

  constructor(public authService: AuthService, private router: Router, private alertify: AlertifyService, private formBuilder: FormBuilder) {
    super();
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.pattern(this.globals.passwordRegex)]],
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

  get loginFormControls() {
    return this.loginForm.controls;
  }

}
