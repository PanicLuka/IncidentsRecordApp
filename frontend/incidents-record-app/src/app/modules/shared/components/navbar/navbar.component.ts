import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  isAuthenticated!: boolean;
  constructor(public userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  isLoggedIn() {
    this.isAuthenticated = this.userService.isAuthenticated();
  }

  public logout() {
    localStorage.removeItem("JWT_NAME");
  }
}
