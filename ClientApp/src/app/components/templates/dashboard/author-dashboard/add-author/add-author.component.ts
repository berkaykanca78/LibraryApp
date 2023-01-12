import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { LanguageEnum } from 'src/app/shared/enums/language';
import { MediaTypeEnum } from 'src/app/shared/enums/mediaType';
import { ProvinceEnum } from 'src/app/shared/enums/province';
import { DateHelper } from 'src/app/shared/helpers/date-helper';
import { Author } from 'src/app/shared/models/author';
import { DropdownModel } from 'src/app/shared/models/dropdownModel';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthorService } from 'src/app/shared/services/author.service';
import { BookService } from 'src/app/shared/services/book.service';
import { CategoryService } from 'src/app/shared/services/category.service';
import { PublisherService } from 'src/app/shared/services/publisher.service';

@Component({
  selector: 'app-add-author',
  templateUrl: './add-author.component.html',
  styleUrls: ['./add-author.component.css']
})
export class AddAuthorComponent implements OnInit {
  author: Author;
  addAuthorForm: FormGroup;
  categoryList = [];
  publisherList = [];
  authorList = [];
  languageKeys = [];
  mediaTypeKeys = [];
  languageEnum = LanguageEnum;
  mediaTypeEnum = MediaTypeEnum;
  provinceKeys = [];
  provinceEnum = ProvinceEnum;
  constructor(private activeModal: NgbActiveModal, private dateHelper: DateHelper, private authorService: AuthorService, private categoryService: CategoryService,
    private publisherService: PublisherService, private alertify: AlertifyService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.addAuthorForm = this.formBuilder.group({
      gender: [0, [Validators.required]],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      dateOfBirth: [this.dateHelper.formatDate(new Date()), [Validators.required]],
      country: [''],
      city: ['', [Validators.required]],
      authorImageUrl: ['', [Validators.required]],
      authorIntroduction: ['', [Validators.required]],
      title: ['', [Validators.required]],
      categoryId: ['', [Validators.required]],
      publisherId: ['', [Validators.required]],
      totalPages: ['', [Validators.required]],
      mediaType: ['', [Validators.required]],
      language: ['', [Validators.required]],
      price: ['', [Validators.required]],
      publishedDate: [this.dateHelper.formatDate(new Date()), [Validators.required]],
      bookImageUrl: ['', [Validators.required]],
      bookIntroduction: ['', [Validators.required]],
    });
    this.setEnums();
  }

  setEnums() {
    this.languageKeys = Object.keys(this.languageEnum);
    this.mediaTypeKeys = Object.keys(this.mediaTypeEnum);
    this.categoryService.getCategoriesForFillDropdown().subscribe(categories => {
      this.categoryList = categories.data as DropdownModel[];
    });
    this.publisherService.getPublishersForFillDropdown().subscribe(publishers => {
      this.publisherList = publishers.data as DropdownModel[];
    });
    this.authorService.getAuthorsForFillDropdown().subscribe(authors => {
      this.authorList = authors.data as DropdownModel[];
    });
    this.provinceKeys = Object.keys(this.provinceEnum);
  }

  closeModal() {
    this.activeModal.close();
  }

  dismissModal() {
    this.activeModal.dismiss();
  }

  addAuthor() {
    this.authorService.addAuthor(this.addAuthorForm.value).subscribe(() => {
      this.alertify.success("Author has already been added.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }

}
