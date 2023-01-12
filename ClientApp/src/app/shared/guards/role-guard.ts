import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { RoleEnum } from "../enums/role";
import { AlertifyService } from "../services/alertify.service";
import { AuthService } from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertifyService: AlertifyService) { }
  canActivate() {
    if (!(this.authService.decodedToken.roleName == RoleEnum.admin)) {
      this.alertifyService.error('You are not authorized to access this page.');
      this.router.navigate(['/home']);
      return false;
    }
    return true;
  }
}
