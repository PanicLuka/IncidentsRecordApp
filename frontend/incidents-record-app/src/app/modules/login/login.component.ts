import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
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

  hide: boolean = true;
  invalidLogin!: boolean;
  email!: string;
  password!: string;
  isLoading = false;
  invalidLoginMess!: boolean;
  user!: User;
  loginCredentials: Login = { email: "", password: "" };
  showSpinner = false;

  constructor(public userService: UserService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    console.log();
  }

  loginForm: FormGroup = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]]
  })


  onLogin(form: FormGroup) {
    if (!this.loginForm.valid) {
      return;
    }
    this.loginCredentials.email = form.value.email;
    this.loginCredentials.password = form.value.password;

    this.userService.loginUser(this.loginCredentials)
      .subscribe(response => {

        const token = response;


        localStorage.setItem("JWT_NAME", token);

        this.invalidLogin = false;
        this.invalidLoginMess = false;

        form.reset();

        this.showSpinner = true;
        setTimeout(() => {
          this.showSpinner = false
          // this.router.navigate(['/incident']);
        }, 1500)

      }), (error: Error) => {
        this.invalidLogin = true;
      };
  }

  /*public login(form: NgForm) {
    this.loginCredentials.email = form.value.email;
    this.loginCredentials.password = form.value.password;

    this.userService.loginUser(this.loginCredentials)
      .subscribe(response => {

        const token = response;


        localStorage.setItem("JWT_NAME", token);

        this.invalidLogin = false;
        this.invalidLoginMess = false;

        form.reset();

          this.showSpinner=true;
          setTimeout(() => {
            this.showSpinner = false
            this.router.navigate(['/']);
          }, 1500)

      }), (error: Error) => {
        this.invalidLogin = true;
      };
  }*/

  removeMessage() {
    this.invalidLoginMess = true;
  }

}
