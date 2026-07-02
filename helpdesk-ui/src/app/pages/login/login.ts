import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {

  @Output() loginSuccess = new EventEmitter<void>();

  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
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

        this.loginSuccess.emit();

      },
      error: (err) => {
        console.log("LOGIN ERROR:", err);
      }
    });
  }
}
