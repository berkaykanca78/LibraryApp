import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ServiceResult } from "../models/serviceResult";

@Injectable({
  providedIn: 'root',
})

export class CategoryService {
  baseUrl: string = "http://localhost:5057/api/Categories/";
  constructor(private http: HttpClient) { }

  getCategoriesForFillDropdown(): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetCategoriesForFillDropdown');
  }

}
