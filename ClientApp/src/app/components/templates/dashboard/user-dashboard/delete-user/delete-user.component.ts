import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {
  @Input() userId: number;

  constructor(private activeModal: NgbActiveModal, private userService: UserService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit(): void {
  }

  closeModal() {
    this.activeModal.close();
  }

  dismissModal() {
    this.activeModal.dismiss();
  }

  deleteUser() {
    this.userService.deleteUser(this.userId).subscribe(() => {
      this.alertify.error("User has already been deleted.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }

}
