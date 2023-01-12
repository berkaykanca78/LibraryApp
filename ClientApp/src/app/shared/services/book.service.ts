import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Book } from "../models/book";
import { ServiceResult } from "../models/serviceResult";

@Injectable({
  providedIn: 'root',
})

export class BookService {
  baseUrl: string = "http://localhost:5057/api/Books/";
  constructor(private http: HttpClient) { }

  getBooks(bookParams?): Observable<ServiceResult> {
    let params = new HttpParams();
    if (bookParams != null) {
      if (bookParams.title != null)
        params = params.append('title', bookParams.title);
      if (bookParams.category != null)
        params = params.append('category', bookParams.category);
      if (bookParams.publisher != null)
        params = params.append('publisher', bookParams.publisher);
      if (bookParams.author != null)
        params = params.append('author', bookParams.author);
      if (bookParams.mediaType != null)
        params = params.append('mediaType', bookParams.mediaType);
      if (bookParams.language != null)
        params = params.append('language', bookParams.language);
      if (bookParams.minRating != null)
        params = params.append('minRating', bookParams.minRating);
      if (bookParams.maxRating != null)
        params = params.append('maxRating', bookParams.maxRating);
      if (bookParams.minPrice != null)
        params = params.append('minPrice', bookParams.minPrice);
      if (bookParams.maxPrice != null)
        params = params.append('maxPrice', bookParams.maxPrice);
      if (bookParams.orderBy != null)
        params = params.append('orderby', bookParams.orderBy);
      if (bookParams.ordering != null)
        params = params.append('ordering', bookParams.ordering);
    }
    return this.http.get<ServiceResult>(this.baseUrl + 'GetBooks', { params: params });
  }

  getBookById(id: number): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetBook/' + id);
  }

  addBook(model: Book) {
    return this.http.post(this.baseUrl + 'AddBook', model);
  }

  updateBook(model: Book) {
    return this.http.put(this.baseUrl + 'UpdateBook', model);
  }

  updateBookRating(id: number, model: any) {
    return this.http.put(this.baseUrl + 'UpdateBookRating/' + id, model);
  }

  deleteBook(id: number) {
    return this.http.delete(this.baseUrl + 'DeleteBook/' + id);
  }
}
