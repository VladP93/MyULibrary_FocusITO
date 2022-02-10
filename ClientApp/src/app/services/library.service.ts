import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class LibraryService {
  private apiUrl = "https://localhost:44318/api/";
  isLogged: boolean = false;

  constructor(private http: HttpClient) {}

  getLogin(credentials: any): Observable<any> {
    return this.http.post(this.apiUrl + "Users/Login", credentials);
  }

  getBooks(): Observable<any> {
    return this.http.get(this.apiUrl + "Books");
  }
}
