import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Book } from "../models/book";
import { AlertifyService } from "../services/alertify.service";
import { AuthService } from "../services/auth.service";
import { BookService } from "../services/book.service";

@Injectable()
export class BookDetailsResolver implements Resolve<Book> {
  constructor(private bookService: BookService, private alertify: AlertifyService, private authService: AuthService,
    private route: Router) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Book | Observable<Book> | Promise<Book> {
    return this.bookService
      .getBookById(route.params['id'])
      .pipe(catchError(error => {
        this.alertify.error("server error");
        this.route.navigate(['/home']);
        return of(null);
      }))
  }

}
