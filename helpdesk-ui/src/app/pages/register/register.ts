import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { RegisterService } from '../../services/register.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  registerForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private registerService: RegisterService,
    private router: Router
  ) {

    this.registerForm = this.fb.group({

      username: ['', Validators.required],

      email: ['', [Validators.required, Validators.email]],

      password: ['', Validators.required]

    });

  }

  register(): void {

    if (this.registerForm.invalid) {
      return;
    }

    this.registerService
      .register(this.registerForm.value)
      .subscribe({

        next: () => {

          alert('Registration Successful!');

          this.router.navigate(['/']);

        },

        error: (err) => {

          alert(err.error.message);

        }

      });

  }

}