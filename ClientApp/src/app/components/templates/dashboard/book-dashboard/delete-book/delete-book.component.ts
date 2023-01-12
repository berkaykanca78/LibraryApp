import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { BookService } from 'src/app/shared/services/book.service';

@Component({
  selector: 'app-delete-book',
  templateUrl: './delete-book.component.html',
  styleUrls: ['./delete-book.component.css']
})
export class DeleteBookComponent implements OnInit {
  @Input() bookId: number;

  constructor(private activeModal: NgbActiveModal, private bookService: BookService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit(): void {
  }

  closeModal() {
    this.activeModal.close();
  }

  dismissModal() {
    this.activeModal.dismiss();
  }

  deleteBook() {
    this.bookService.deleteBook(this.bookId).subscribe(() => {
      this.alertify.error("Book has already been deleted.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }

}
