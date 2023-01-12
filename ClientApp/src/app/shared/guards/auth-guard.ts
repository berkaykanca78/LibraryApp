import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AlertifyService } from "../services/alertify.service";
import { AuthService } from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertifyService: AlertifyService) { }
  canActivate() {
    if (!this.authService.loggedIn()) {
      this.alertifyService.error('Your session has expired. Please login again.');
      this.router.navigate(['/home']);
      return false;
    }

    return true;
  }
}
