import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  hide: boolean = true;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    console.log('register')

  }

  registerForm: FormGroup = this.formBuilder.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]]
  })


  onRegister() {
    if (!this.registerForm.valid) {
      return;
    }
    console.log(this.registerForm.value);
  }

}
