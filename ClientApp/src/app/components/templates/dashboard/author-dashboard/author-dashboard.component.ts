import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Author } from 'src/app/shared/models/author';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthorService } from 'src/app/shared/services/author.service';
import { AddAuthorComponent } from './add-author/add-author.component';
import { DeleteAuthorComponent } from './delete-author/delete-author.component';
import { UpdateAuthorComponent } from './update-author/update-author.component';

@Component({
  selector: 'app-author-dashboard',
  templateUrl: './author-dashboard.component.html',
  styleUrls: ['./author-dashboard.component.css']
})
export class AuthorDashboardComponent implements OnInit {
  authors = [];
  pageOfItems: Array<any>;

  constructor(private authorService: AuthorService, private alertify: AlertifyService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.getAuthors();
  }

  getAuthors() {
    setTimeout(() => {
      this.authorService.getAuthors().subscribe(result => {
        this.authors = result.data.map(x => ({
          id: (x.id), fullName: (x.fullName), country: (x.country), city: (x.city),
          dateOfBirth: (x.dateOfBirth), imageUrl: (x.imageUrl), isFavourite: (x.isFavourite),
          age: (x.age), genderName: (x.genderName), no: (x.no), firstName: (x.firstName), lastName: (x.lastName),
          introduction: (x.introduction), gender: (x.gender),
        })) as Author[];
      }, err => {
        this.alertify.error(err);
      });
    }, 1000);
  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }
  openAddAuthorModal() {
    this.modalService.open(AddAuthorComponent);
  }
  openUpdateAuthorModal(author: Author) {
    const modalRef = this.modalService.open(UpdateAuthorComponent);
    modalRef.componentInstance.author = author;
  }
  openDeleteAuthorModal(authorId: number) {
    const modalRef = this.modalService.open(DeleteAuthorComponent);
    modalRef.componentInstance.authorId = authorId;
  }
}
