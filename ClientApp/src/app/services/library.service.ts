import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class LibraryService {
  private apiUrl = "https://localhost:44318/api/";
  isLogged: boolean = false;
  rolLogged: number = 0;
  userLogged: number = 0;

  constructor(private http: HttpClient) {}

  getLogin(credentials: any): Observable<any> {
    return this.http.post(this.apiUrl + "Users/Login", credentials);
  }

  getBooks(): Observable<any> {
    return this.http.get(this.apiUrl + "Books");
  }

  getBook(id: number): Observable<any> {
    return this.http.get(this.apiUrl + "Books/" + id);
  }

  getGenres(): Observable<any> {
    return this.http.get(this.apiUrl + "Genres");
  }

  createBook(book: any): Observable<any> {
    return this.http.post(this.apiUrl + "Books", book);
  }

  updateBook(id: number, book: any): Observable<any> {
    return this.http.put(this.apiUrl + "Books/" + id, book);
  }

  getBookRegistries(): Observable<any> {
    return this.http.get(this.apiUrl + "BookRegistries");
  }

  getBookRegistry(id: number): Observable<any> {
    return this.http.get(this.apiUrl + "BookRegistries/" + id);
  }

  createBookRegistry(bookregistry: any): Observable<any> {
    return this.http.post(this.apiUrl + "BookRegistries", bookregistry);
  }

  returnToLibrary(id: number, bookregistry: any): Observable<any> {
    console.log(bookregistry);
    return this.http.put(this.apiUrl + "BookRegistries/" + id, bookregistry);
  }
}
