import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ServiceResult } from "../models/serviceResult";
import { User } from "../models/user";

@Injectable({
  providedIn: 'root',
})

export class UserService {
  baseUrl: string = "http://localhost:5057/api/Users/";
  constructor(private http: HttpClient) { }

  getUsers(userParams?): Observable<ServiceResult> {
    let params = new HttpParams();
    if (userParams != null) {
      if (userParams.name != null)
        params = params.append('name', userParams.name);
      if (userParams.surname != null)
        params = params.append('surname', userParams.surname);
      if (userParams.userName != null)
        params = params.append('userName', userParams.userName);
      if (userParams.gender != null)
        params = params.append('gender', userParams.gender);
      if (userParams.city != null)
        params = params.append('city', userParams.city);
      if (userParams.minAge != null)
        params = params.append('minAge', userParams.minAge);
      if (userParams.maxAge != null)
        params = params.append('maxAge', userParams.maxAge);
      if (userParams.orderBy != null)
        params = params.append('orderBy', userParams.orderBy);
      if (userParams.ordering != null)
        params = params.append('ordering', userParams.ordering);
    }
    return this.http.get<ServiceResult>(this.baseUrl + 'GetUsers', { params: params });
  }

  getUserById(id: number): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetUser/' + id);
  }

  getUsersForDashboard(): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetUsersForDashboard');
  }

  addUser(user: User) {
    return this.http.post(this.baseUrl + 'AddUser', user);
  }

  updateUser(user: User) {
    return this.http.put(this.baseUrl + 'UpdateUser', user);
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + 'DeleteUser/' + id);
  }
}
