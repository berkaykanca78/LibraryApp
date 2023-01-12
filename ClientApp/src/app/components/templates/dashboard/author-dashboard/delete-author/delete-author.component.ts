import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertifyService } from 'src/app/shared/services/alertify.service';
import { AuthorService } from 'src/app/shared/services/author.service';

@Component({
  selector: 'app-delete-author',
  templateUrl: './delete-author.component.html',
  styleUrls: ['./delete-author.component.css']
})
export class DeleteAuthorComponent implements OnInit {
  @Input() authorId: number;

  constructor(private activeModal: NgbActiveModal, private authorService: AuthorService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit(): void {
  }

  closeModal() {
    this.activeModal.close();
  }

  dismissModal() {
    this.activeModal.dismiss();
  }

  deleteAuthor() {
    this.authorService.deleteAuthor(this.authorId).subscribe(() => {
      this.alertify.error("Author has already been deleted.");
      this.activeModal.close();
      this.router.navigate(['/dashboard']);
    });
  }
}
