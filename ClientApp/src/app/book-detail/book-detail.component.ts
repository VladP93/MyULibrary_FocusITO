import { ThrowStmt } from "@angular/compiler";
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
      console.log(d);
      this.book = d;
    });
  }

  requestBook(id: number) {
    let bookObj = {
      idbook: this.book.idbook,
      title: this.book.title,
      author: this.book.author,
      stock: this.book.stock - 1,
      idGenre: this.book.idGenre,
      description: this.book.description,
      imageURL: this.book.imageURL,
      publishedYear: this.book.publishedYear,
    };
    this._libraryService.updateBook(id, bookObj).subscribe((d) => {
      this.requested = true;
    });

    this.getBookData(id);
  }
}
