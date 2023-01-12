import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DateHelper } from 'src/app/shared/helpers/date-helper';
import { Author } from 'src/app/shared/models/author';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthorService } from 'src/app/shared/services/author.service';

@Component({
  selector: 'app-update-author',
  templateUrl: './update-author.component.html',
  styleUrls: ['./update-author.component.css']
})
export class UpdateAuthorComponent implements OnInit {
  @Input() author: Author;
  updateAuthorForm: FormGroup;
  constructor(private activeModal: NgbActiveModal, private dateHelper: DateHelper, private authorService: AuthorService,
    private alertify: AlertifyService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.updateAuthorForm = this.formBuilder.group({
      id: [this.author.id],
      gender: [this.author.gender, [Validators.required]],
      firstName: [this.author.firstName, [Validators.required]],
      lastName: [this.author.lastName, [Validators.required]],
      dateOfBirth: [this.dateHelper.formatDate(this.author.dateOfBirth), [Validators.required]],
      country: [this.author.country, [Validators.required]],
      city: [this.author.city, [Validators.required]],
      imageUrl: [this.author.imageUrl, [Validators.required]],
      introduction: [this.author.introduction, [Validators.required]],
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  dismissModal() {
    this.activeModal.dismiss();
  }

  updateAuthor() {
    this.authorService.updateAuthor(this.updateAuthorForm.value).subscribe(() => {
      this.alertify.success("Author has already been updated.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }

}
