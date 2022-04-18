import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Login } from 'src/app/models/login.model';
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
  loginCredentials: Login = { email: "", password: "" };

  constructor(public userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  public login(form: NgForm) {
    this.loginCredentials.email = form.value.email;
    this.loginCredentials.password = form.value.password;

    this.userService.loginUser(this.loginCredentials)
      .subscribe(response => {

        const token = response;


        localStorage.setItem("JWT_NAME", token);

        console.log(token);

        this.invalidLogin = false;
        this.invalidLoginMess = false;

        form.reset();

        setTimeout(() => {
          this.router.navigate(['/']);
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
