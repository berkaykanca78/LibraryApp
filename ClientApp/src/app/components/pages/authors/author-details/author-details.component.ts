import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { Author } from 'src/app/shared/models/author';

@Component({
  selector: 'app-author-details',
  templateUrl: './author-details.component.html',
  styleUrls: ['./author-details.component.css']
})
export class AuthorDetailsComponent extends BaseComponent implements OnInit {
  author: Author;
  constructor(private route: ActivatedRoute) {
    super();
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.author = data.author.data;
    })
  }
}
