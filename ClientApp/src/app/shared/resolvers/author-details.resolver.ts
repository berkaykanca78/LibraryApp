import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Author } from "../models/author";
import { AlertifyService } from "../services/alertify.service";
import { AuthService } from "../services/auth.service";
import { AuthorService } from "../services/author.service";

@Injectable()
export class AuthorDetailsResolver implements Resolve<Author> {
  constructor(private authorService: AuthorService, private alertify: AlertifyService, private authService: AuthService,
    private route: Router) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Author | Observable<Author> | Promise<Author> {
    return this.authorService
      .getAuthorById(route.params['id'])
      .pipe(catchError(error => {
        this.alertify.error("server error");
        this.route.navigate(['/home']);
        return of(null);
      }))
  }

}
