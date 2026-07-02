import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './pages/login/login';
import { Tickets } from './pages/ticket/ticket';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, LoginComponent, Tickets],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  showLogin = true;

  onLoginSuccess() {
    this.showLogin = false;
  }
}