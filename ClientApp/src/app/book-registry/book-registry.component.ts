import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { LibraryService } from "../services/library.service";

@Component({
  selector: "app-book-registry",
  templateUrl: "./book-registry.component.html",
  styleUrls: ["./book-registry.component.css"],
})
export class BookRegistryComponent implements OnInit {
  bookregistries: any = [];
  showReturnedBooks: boolean = false;

  constructor(
    private _libraryService: LibraryService,
    private router: Router
  ) {}

  ngOnInit() {
    if (
      !this._libraryService.isLogged &&
      this._libraryService.userLogged != 1
    ) {
      this.router.navigate([""]);
    }
    this.getRegistries();
  }

  showReturned(event) {
    this.showReturnedBooks = event.target.checked;
  }

  returnBook(idBookRegistry: number) {
    this._libraryService.getBookRegistry(idBookRegistry).subscribe((d) => {
      var bookRegistry = {
        idBookRegistry: idBookRegistry,
        idStudent: d.idStudent,
        idBook: d.idBook,
        dateCheckout: d.dateCheckout,
        dateReturn: new Date(),
        returned: true,
      };
      this._libraryService
        .returnToLibrary(idBookRegistry, bookRegistry)
        .subscribe((d) => {
          this.getRegistries();
        });
    });
  }

  getRegistries() {
    this._libraryService.getBookRegistries().subscribe((d) => {
      console.log(d);
      this.bookregistries = d;
    });
  }
}
