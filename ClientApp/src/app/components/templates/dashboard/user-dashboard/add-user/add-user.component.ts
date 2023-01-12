import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { ProvinceEnum } from 'src/app/shared/enums/province';
import { DateHelper } from 'src/app/shared/helpers/date-helper';
import { User } from 'src/app/shared/models/user';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent extends BaseComponent implements OnInit {
  user: User;
  addUserForm: FormGroup;
  provinceKeys = [];
  provinceEnum = ProvinceEnum;

  constructor(private activeModal: NgbActiveModal, private userService: UserService, private alertify: AlertifyService, public dateHelper: DateHelper,
    private router: Router, private formBuilder: FormBuilder) {
    super();
  }

  ngOnInit(): void {
    this.addUserForm = this.formBuilder.group({
      gender: [0, [Validators.required]],
      name: ['', [Validators.required]],
      surname: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern(this.globals.passwordRegex)]],
      userName: ['', [Validators.required]],
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

  closeModal() {
    this.activeModal.close();
  }

  dismissModal() {
    this.activeModal.dismiss();
  }

  addUser() {
    this.userService.addUser(this.addUserForm.getRawValue()).subscribe(() => {
      this.alertify.success("User has already been added.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }

  get addUserFormControls() {
    return this.addUserForm.controls;
  }
}
