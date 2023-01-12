import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { LanguageEnum } from 'src/app/shared/enums/language';
import { MediaTypeEnum } from 'src/app/shared/enums/mediaType';
import { Book } from 'src/app/shared/models/book';
import { DropdownModel } from 'src/app/shared/models/dropdownModel';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthorService } from 'src/app/shared/services/author.service';
import { BookService } from 'src/app/shared/services/book.service';
import { CategoryService } from 'src/app/shared/services/category.service';
import { PublisherService } from 'src/app/shared/services/publisher.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent extends BaseComponent implements OnInit {
  public loading = false;
  languageEnum = LanguageEnum;
  mediaTypeEnum = MediaTypeEnum;
  categoryList = [];
  publisherList = [];
  authorList = [];
  languageKeys = [];
  mediaTypeKeys = [];
  books = [];
  bookParams: any = {};
  pageOfItems: Array<any>;
  rate: any = 0;
  options = {
    maxRating: 5,
    readOnly: false,
    resetAllowed: true
  }

  constructor(private bookService: BookService, private alertify: AlertifyService, private categoryService: CategoryService,
    private publisherService: PublisherService, private authorService: AuthorService) {
    super();
  }

  ngOnInit(): void {
    this.bookParams.orderBy = "created";
    this.bookParams.ordering = "desc";
    this.getBooks();
  }

  onRatingChanged(event, book: any) {
    book.rating = (event.oldRating + event.newRating) / 2;
    this.bookService.updateBookRating(book.id, book).subscribe(() => {
      this.alertify.success("Book's rating has been updated");
      this.getBooks();
    });
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

  getBooks() {
    this.loading = true;
    this.setEnums();
    setTimeout(() => {
      this.loading = false;
      this.bookService.getBooks(this.bookParams).subscribe(result => {
        this.books = result.data as Book[];
      }, err => {
        this.alertify.error(err);
      });
    }, 1000)
  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }
}


