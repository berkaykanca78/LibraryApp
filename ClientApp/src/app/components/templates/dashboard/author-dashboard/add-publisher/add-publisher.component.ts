import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { PublisherService } from 'src/app/shared/services/publisher.service';

@Component({
  selector: 'app-add-publisher',
  templateUrl: './add-publisher.component.html',
  styleUrls: ['./add-publisher.component.css']
})
export class AddPublisherComponent implements OnInit {
  addPublisherForm: FormGroup;
  constructor(private activeModal: NgbActiveModal, private alertify: AlertifyService, private router: Router, private formBuilder: FormBuilder,
    private publisherService: PublisherService) { }

  ngOnInit(): void {
    this.addPublisherForm = this.formBuilder.group({
      name: ['', [Validators.required]],
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  dismissModal() {
    this.activeModal.dismiss();
  }

  addPublisher() {
    this.publisherService.addPublisher(this.addPublisherForm.getRawValue()).subscribe(() => {
      this.alertify.success("Publisher has already been added.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }
}
