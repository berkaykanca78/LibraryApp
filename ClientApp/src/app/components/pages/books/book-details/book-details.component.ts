import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { Book } from 'src/app/shared/models/book';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent extends BaseComponent implements OnInit {
  book: Book;
  constructor(private route: ActivatedRoute) {
    super();
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.book = data.book.data;
    })
  }

}
