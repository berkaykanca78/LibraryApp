import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { ProvinceEnum } from 'src/app/shared/enums/province';
import { DateHelper } from 'src/app/shared/helpers/date-helper';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent extends BaseComponent implements OnInit {
  registerForm: FormGroup;
  provinceKeys = [];
  provinceEnum = ProvinceEnum;

  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService, private formBuilder: FormBuilder, public dateHelper: DateHelper) {
    super();
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      gender: [0, [Validators.required]],
      name: ['', [Validators.required]],
      surname: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern(this.globals.passwordRegex)]],
      username: ['', [Validators.required]],
      dateOfBirth: [this.dateHelper.formatDate(new Date()), [Validators.required]],
      country: [''],
      city: ['', [Validators.required]],
      streetName: ['', [Validators.required]],
      zipCode: ['', [Validators.required]],
      fullAddress: ['', [Validators.required]],
      introduction: ['', [Validators.required]],
      hobbies: ['', [Validators.required]],
      imageUrl: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required, Validators.pattern(this.globals.phoneRegex)]],
    });
    this.setEnums();
  }

  setEnums() {
    this.provinceKeys = Object.keys(this.provinceEnum);
  }

  register() {
    this.authService.register(this.registerForm.getRawValue()).subscribe(() => {
      this.alertify.success("User created successfully");
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.authService.login(this.registerForm.getRawValue()).subscribe(() => {
        this.router.navigate(['/books']);
      });
    });
  }

  get registerFormControls() {
    return this.registerForm.controls;
  }
}
