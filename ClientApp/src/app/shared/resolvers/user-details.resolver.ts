import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { User } from "../models/user";
import { AlertifyService } from "../services/alertify.service";
import { AuthService } from "../services/auth.service";
import { UserService } from "../services/user.service";

@Injectable()
export class UserDetailsResolver implements Resolve<User> {
  constructor(private userService: UserService, private alertify: AlertifyService, private authService: AuthService,
    private route: Router) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
    return this.userService
      .getUserById(route.params['id'])
      .pipe(catchError(error => {
        this.alertify.error("server error");
        this.route.navigate(['/home']);
        return of(null);
      }))
  }

}
