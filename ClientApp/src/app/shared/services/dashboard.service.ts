import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ServiceResult } from '../models/serviceResult';

@Injectable({
  providedIn: 'root',
})

export class DashboardService {
  baseUrl: string = "http://localhost:5057/api/Dashboard/";

  constructor(private http: HttpClient) { }

  getCountsForDashboard(): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetCountsForDashboard');
  }

  getCategoriesForBooksInfo(): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetCategoriesForBooksInfo');
  }

  getCitiesForUsersInfo(): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetCitiesForUsersInfo');
  }

  getBooksForAuthorsInfo(): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetBooksForAuthorsInfo');
  }
}
