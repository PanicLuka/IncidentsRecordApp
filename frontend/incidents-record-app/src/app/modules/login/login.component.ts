import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  invalidLogin!:boolean;
  email!: string;
  password!: string;
  isLoading = false;
  invalidLoginMess!: boolean;
  user!: User;

  constructor(public userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  public login(form: NgForm) {
    this.email = form.value.email;
    this.password = form.value.password;

    const credentials = {
      "email": this.email,
      "password": this.password
    }

    this.user.email=credentials.email;
    this.user.password=credentials.password;

    this.userService.loginUser(this.user)
      .subscribe(response => {

        const token = (<any>response).token;


        localStorage.setItem("JWT ", token);

        this.invalidLogin = false;
        this.invalidLoginMess = false;

        form.reset();

        setTimeout(() => {
          this.router.navigate(['/login']);
        },
          2500);

      }), (error: Error) => {
        this.invalidLogin = true;
      };
  }

  removeMessage() {
    this.invalidLoginMess = true;
  }

}
