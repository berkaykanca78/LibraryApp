import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Book } from 'src/app/shared/models/book';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { BookService } from 'src/app/shared/services/book.service';
import { AddPublisherComponent } from '../author-dashboard/add-publisher/add-publisher.component';
import { AddBookComponent } from './add-book/add-book.component';
import { DeleteBookComponent } from './delete-book/delete-book.component';
import { UpdateBookComponent } from './update-book/update-book.component';

@Component({
  selector: 'app-book-dashboard',
  templateUrl: './book-dashboard.component.html',
  styleUrls: ['./book-dashboard.component.css']
})
export class BookDashboardComponent implements OnInit {
  books = [];
  pageOfItems: Array<any>;

  constructor(private bookService: BookService, private alertify: AlertifyService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks() {
    setTimeout(() => {
      this.bookService.getBooks().subscribe(result => {
        this.books = result.data as Book[];
      }, err => {
        this.alertify.error(err);
      });
    }, 1000)
  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }

  openAddPublisherModal() {
    this.modalService.open(AddPublisherComponent);
  }

  openAddBookModal() {
    this.modalService.open(AddBookComponent);
  }
  openUpdateBookModal(book: Book) {
    const modalRef = this.modalService.open(UpdateBookComponent);
    modalRef.componentInstance.book = book;
  }
  openDeleteBookModal(bookId: number) {
    const modalRef = this.modalService.open(DeleteBookComponent);
    modalRef.componentInstance.bookId = bookId;
  }

}
