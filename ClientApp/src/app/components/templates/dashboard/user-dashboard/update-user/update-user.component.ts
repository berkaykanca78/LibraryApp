import { Component, Input, OnInit } from '@angular/core';
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
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent extends BaseComponent implements OnInit {
  @Input() user: User;
  updateUserForm: FormGroup;
  provinceKeys = [];
  provinceEnum = ProvinceEnum;

  constructor(private activeModal: NgbActiveModal, private userService: UserService, private alertify: AlertifyService,
    public dateHelper: DateHelper, private router: Router, private formBuilder: FormBuilder) {
    super();
  }

  ngOnInit(): void {
    this.updateUserForm = this.formBuilder.group({
      id: [this.user.id],
      gender: [this.user.gender, [Validators.required]],
      name: [this.user.name, [Validators.required]],
      surname: [this.user.surname, [Validators.required]],
      email: [this.user.email, [Validators.required, Validators.email]],
      userName: [this.user.userName, [Validators.required]],
      dateOfBirth: [this.dateHelper.formatDate(this.user.dateOfBirth), [Validators.required]],
      country: [''],
      city: [this.user.city, [Validators.required]],
      streetName: [this.user.streetName, [Validators.required]],
      zipCode: [this.user.zipCode, [Validators.required]],
      fullAddress: [this.user.fullAddress, [Validators.required]],
      introduction: [this.user.introduction, [Validators.required]],
      hobbies: [this.user.hobbies, [Validators.required]],
      imageUrl: [this.user.imageUrl, [Validators.required]],
      phoneNumber: [this.user.phoneNumber, [Validators.required, Validators.pattern(this.globals.phoneRegex)]],
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

  updateUser() {
    this.userService.updateUser(this.updateUserForm.getRawValue()).subscribe(() => {
      this.alertify.success("User has already been updated.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }

  get updateUserFormControls() {
    return this.updateUserForm.controls;
  }
}
