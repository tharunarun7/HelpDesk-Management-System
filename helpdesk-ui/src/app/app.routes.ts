import { Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login';
import { Register } from './pages/register/register';
import { Tickets } from './pages/ticket/ticket';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [

  {
    path: '',
    component: LoginComponent
  },

  {
    path: 'register',
    component: Register
  },

  {
    path: 'ticket',
    component: Tickets,
    canActivate: [authGuard]
  },

  {
    path: '**',
    redirectTo: ''
  }

];
