import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Author } from "../models/author";
import { ServiceResult } from "../models/serviceResult";

@Injectable({
  providedIn: 'root',
})

export class AuthorService {
  baseUrl: string = "http://localhost:5057/api/Authors/";
  constructor(private http: HttpClient) { }

  getAuthors(authorParams?): Observable<ServiceResult> {
    let params = new HttpParams();
    if (authorParams != null) {
      if (authorParams.name != null)
        params = params.append('name', authorParams.name);
      if (authorParams.surname != null)
        params = params.append('surname', authorParams.surname);
      if (authorParams.nationality != null)
        params = params.append('nationality', authorParams.nationality);
      if (authorParams.gender != null)
        params = params.append('gender', authorParams.gender);
      if (authorParams.minAge != null)
        params = params.append('minAge', authorParams.minAge);
      if (authorParams.maxAge != null)
        params = params.append('maxAge', authorParams.maxAge);
      if (authorParams.orderBy != null)
        params = params.append('orderby', authorParams.orderBy);
      if (authorParams.ordering != null)
        params = params.append('ordering', authorParams.ordering);
    }
    return this.http.get<ServiceResult>(this.baseUrl + 'GetAuthors', { params: params });
  }

  getAuthorsForFillDropdown(): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetAuthorsForFillDropdown');
  }

  getAuthorById(id: number): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetAuthor/' + id);
  }

  addAuthor(model: Author) {
    return this.http.post(this.baseUrl + 'AddAuthor', model);
  }

  updateAuthor(author: Author) {
    return this.http.put(this.baseUrl + 'UpdateAuthor', author);
  }

  deleteAuthor(id: number) {
    return this.http.delete(this.baseUrl + 'DeleteAuthor/' + id);
  }

  addOrUpdateAuthorToFavourite(author: Author) {
    return this.http.put(this.baseUrl + 'AddOrUpdateAuthorToFavourite', author);
  }
}
