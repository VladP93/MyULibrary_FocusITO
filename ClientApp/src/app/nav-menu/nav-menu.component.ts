import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { LibraryService } from "../services/library.service";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
})
export class NavMenuComponent {
  constructor(public _libraryService: LibraryService, private router: Router) {}

  isExpanded = false;
  isLogged = this._libraryService.isLogged;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this._libraryService.isLogged = false;
    this._libraryService.userLogged = 0;
    this._libraryService.rolLogged = 0;
    this.router.navigate([""]);
  }
}
