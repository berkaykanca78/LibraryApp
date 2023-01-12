import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { LanguageEnum } from 'src/app/shared/enums/language';
import { MediaTypeEnum } from 'src/app/shared/enums/mediaType';
import { DateHelper } from 'src/app/shared/helpers/date-helper';
import { Book } from 'src/app/shared/models/book';
import { DropdownModel } from 'src/app/shared/models/dropdownModel';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthorService } from 'src/app/shared/services/author.service';
import { BookService } from 'src/app/shared/services/book.service';
import { CategoryService } from 'src/app/shared/services/category.service';
import { PublisherService } from 'src/app/shared/services/publisher.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
  book: Book;
  addBookForm: FormGroup;
  categoryList = [];
  publisherList = [];
  authorList = [];
  languageKeys = [];
  mediaTypeKeys = [];
  languageEnum = LanguageEnum;
  mediaTypeEnum = MediaTypeEnum;

  constructor(private activeModal: NgbActiveModal, private dateHelper: DateHelper, private authorService: AuthorService, private categoryService: CategoryService, private publisherService: PublisherService, private bookService: BookService, private alertify: AlertifyService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.addBookForm = this.formBuilder.group({
      title: ['', [Validators.required]],
      categoryId: ['', [Validators.required]],
      publisherId: ['', [Validators.required]],
      authorId: ['', [Validators.required]],
      mediaType: ['', [Validators.required]],
      language: ['', [Validators.required]],
      totalPages: ['', [Validators.required]],
      price: ['', [Validators.required]],
      publishedDate: [this.dateHelper.formatDate(new Date()), [Validators.required]],
      imageUrl: ['', [Validators.required]],
      introduction: ['', [Validators.required]],
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
  }
  closeModal() {
    this.activeModal.close();
  }

  dismissModal() {
    this.activeModal.dismiss();
  }

  addBook() {
    this.bookService.addBook(this.addBookForm.getRawValue()).subscribe(() => {
      this.alertify.success("Book has already been added.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }

}
