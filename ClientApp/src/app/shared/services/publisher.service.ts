import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Publisher } from "../models/publisher";
import { ServiceResult } from "../models/serviceResult";

@Injectable({
  providedIn: 'root',
})

export class PublisherService {
  baseUrl: string = "http://localhost:5057/api/Publishers/";
  constructor(private http: HttpClient) { }

  getPublishersForFillDropdown(): Observable<ServiceResult> {
    return this.http.get<ServiceResult>(this.baseUrl + 'GetPublishersForFillDropdown');
  }

  addPublisher(publisher: Publisher) {
    return this.http.post(this.baseUrl + 'AddPublisher', publisher);
  }

}
