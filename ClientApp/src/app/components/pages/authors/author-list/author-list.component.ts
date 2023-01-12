import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BaseComponent } from 'src/app/shared/bases/base.component';
import { Author } from 'src/app/shared/models/author';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthorService } from 'src/app/shared/services/author.service';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css']
})
export class AuthorListComponent extends BaseComponent implements OnInit {
  authors: Author[];
  public loading = false;
  authorParams: any = {};
  form: FormGroup;
  pageOfItems: Array<any>;

  constructor(private authorService: AuthorService, private alertify: AlertifyService) {
    super();
  }

  ngOnInit(): void {
    this.authorParams.orderBy = "age";
    this.authorParams.ordering = "desc";
    this.getAuthors();
  }

  getAuthors() {
    this.loading = true;
    setTimeout(() => {
      this.loading = false;
      this.authorService.getAuthors(this.authorParams).subscribe(result => {
        this.authors = result.data.map(x => ({
          id: (x.id), fullName: (x.fullName), country: (x.country), city: (x.city),
          dateOfBirth: (x.dateOfBirth), imageUrl: (x.imageUrl), isFavourite: (x.isFavourite)
        })) as Author[];
      }, err => {
        this.alertify.error(err);
      });
    }, 1000);
  }

  toggleFavorite(author: Author) {
    this.authorService.addOrUpdateAuthorToFavourite(author).subscribe(() => {
      this.alertify.success("Author's favourite has been updated");
      this.getAuthors();
    });
  }

  onChangePageForAuthor(pageOfItemsForAuthor: Array<any>) {
    this.pageOfItems = pageOfItemsForAuthor;
  }
}
