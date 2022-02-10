import { Component } from "@angular/core";
import { LibraryService } from "../services/library.service";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
})
export class NavMenuComponent {
  constructor(public _libraryService: LibraryService) {}

  isExpanded = false;
  isLogged = this._libraryService.isLogged;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
