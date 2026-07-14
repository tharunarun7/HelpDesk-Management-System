import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {

  

  loginForm: FormGroup;

  constructor(
  private fb: FormBuilder,
  private authService: AuthService,
  private router: Router
) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

 login(): void {

  this.authService.login(this.loginForm.value).subscribe({

    next: (res: any) => {

      console.log("LOGIN SUCCESS:", res);

      alert("Login Successful!");

      this.router.navigate(['/ticket']);

    },

    error: (err) => {

      console.log("LOGIN ERROR:", err);

    }

  });

}}