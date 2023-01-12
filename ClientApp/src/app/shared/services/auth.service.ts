import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RoleEnum } from '../enums/role';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  baseUrl: string = "http://localhost:5057/api/Auth/";
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'Login', model).pipe(
      map((response: any) => {
        const result = response;
        if (result) {
          localStorage.setItem("token", result.token);
          this.decodedToken = this.jwtHelper.decodeToken(result.token);
        }
      })
    )
  }

  register(model: any) {
    console.log(model);
    return this.http.post(this.baseUrl + 'Register', model);
  }

  loggedIn() {
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }

  isAdmin() {
    return this.decodedToken.roleName == RoleEnum.admin;
  }
}
