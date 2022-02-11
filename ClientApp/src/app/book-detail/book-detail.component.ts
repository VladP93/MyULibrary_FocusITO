import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { LibraryService } from "../services/library.service";

interface Book {
  idbook: number;
  title: string;
  author: string;
  genre: string;
  idGenre: number;
  publishedYear: string;
  stock: number;
  imageURL: string;
  description: string;
}

@Component({
  selector: "app-book-detail",
  templateUrl: "./book-detail.component.html",
  styleUrls: ["./book-detail.component.css"],
})
export class BookDetailComponent implements OnInit {
  book: Book = {
    idbook: 0,
    title: "",
    author: "",
    genre: "",
    idGenre: 0,
    publishedYear: "",
    stock: 0,
    imageURL: "",
    description: "",
  };

  requested: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private _libraryService: LibraryService
  ) {}

  ngOnInit() {
    let id = this.route.snapshot.params.id;

    this.getBookData(id);
  }

  getBookData(id: number) {
    this._libraryService.getBook(id).subscribe((d) => {
      this.book = d;
    });
  }

  requestBook(id: number) {
    let bookUpdate = {
      idbook: this.book.idbook,
      title: this.book.title,
      author: this.book.author,
      stock: this.book.stock - 1,
      idGenre: this.book.idGenre,
      description: this.book.description,
      imageURL: this.book.imageURL,
      publishedYear: this.book.publishedYear,
    };

    let date = new Date();

    let bookRegistry = {
      dateCheckout: date,
      dateReturn: date,
      returned: false,
      idBook: id,
      idStudent: this._libraryService.userLogged,
    };

    this._libraryService.updateBook(id, bookUpdate).subscribe((d) => {
      this.requested = true;
    });

    this._libraryService.createBookRegistry(bookRegistry).subscribe((d) => {});

    this.getBookData(id);
  }
}
