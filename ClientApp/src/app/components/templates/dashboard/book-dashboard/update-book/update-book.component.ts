import { Component, Input, OnInit } from '@angular/core';
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
  selector: 'app-update-book',
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.css']
})
export class UpdateBookComponent implements OnInit {
  @Input() book: Book;
  updateBookForm: FormGroup;
  categoryList = [];
  publisherList = [];
  authorList = [];
  languageKeys = [];
  mediaTypeKeys = [];
  languageEnum = LanguageEnum;
  mediaTypeEnum = MediaTypeEnum;
  constructor(private activeModal: NgbActiveModal, private dateHelper: DateHelper, private authorService: AuthorService, private categoryService: CategoryService,
    private publisherService: PublisherService, private bookService: BookService, private alertify: AlertifyService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.updateBookForm = this.formBuilder.group({
      id: [this.book.id],
      title: [this.book.title, [Validators.required]],
      categoryId: [this.book.categoryId, [Validators.required]],
      publisherId: [this.book.publisherId, [Validators.required]],
      authorId: [this.book.authorId, [Validators.required]],
      mediaType: [this.book.mediaType, [Validators.required]],
      language: [this.book.language, [Validators.required]],
      totalPages: [this.book.totalPages, [Validators.required]],
      price: [this.book.price, [Validators.required]],
      publishedDate: [this.dateHelper.formatDate(this.book.publishedDate), [Validators.required]],
      imageUrl: [this.book.imageUrl, [Validators.required]],
      introduction: [this.book.introduction, [Validators.required]],
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

  updateBook() {
    this.bookService.updateBook(this.updateBookForm.getRawValue()).subscribe(() => {
      this.alertify.success("Book has already been updated.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }

}
