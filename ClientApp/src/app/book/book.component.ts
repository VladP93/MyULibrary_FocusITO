import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { LibraryService } from "../services/library.service";

@Component({
  selector: "app-book",
  templateUrl: "./book.component.html",
  styleUrls: ["./book.component.css"],
})
export class BookComponent implements OnInit {
  books: any = [];
  genres: any = [];
  form: FormGroup;
  errorMessage: string;

  constructor(
    private _libraryService: LibraryService,
    private fb: FormBuilder
  ) {
    this.form = this.fb.group({
      title: "",
      author: "",
      stock: "",
      publishedYear: "",
      idGenre: "",
      imageURL: "",
      description: "",
    });
  }

  ngOnInit() {
    this.getBooklist();
    this.getGenres();
  }

  getBooklist() {
    this._libraryService.getBooks().subscribe((d) => {
      this.books = d;
    });
  }

  getGenres() {
    this._libraryService.getGenres().subscribe((d) => {
      this.genres = d;
    });
  }

  saveBook() {
    this.errorMessage = "";
    let book = this.form.value;
    book.idGenre = parseInt(book.idGenre);
    let errorQ = false;
    if (!book.title) {
      this.errorMessage = "No title ";
      errorQ = true;
    }
    if (!book.author) {
      this.errorMessage += "No author ";
      errorQ = true;
    }
    if (!book.stock) {
      this.errorMessage += "No stock ";
      errorQ = true;
    }
    if (!book.publishedYear) {
      this.errorMessage += "No published date ";
      errorQ = true;
    }
    if (!book.idGenre) {
      this.errorMessage += "No Genre ";
      errorQ = true;
    }
    if (!errorQ) {
      this._libraryService.createBook(book).subscribe((d) => {
        console.log(d);
        this.form.reset();
        this.getBooklist();
      });
    }
  }
}
