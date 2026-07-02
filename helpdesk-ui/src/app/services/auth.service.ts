import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from '../models/login-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7193/api/Auth';

  constructor(private http: HttpClient) { }

  login(loginRequest: LoginRequest): Observable<any> {
  return new Observable(observer => {
    this.http.post(`${this.apiUrl}/login`, loginRequest)
      .subscribe({
        next: (res: any) => {

          // ✅ STORE TOKEN HERE
          localStorage.setItem('token', res.token);
localStorage.setItem('userId', res.id.toString());
localStorage.setItem('username', res.username);
localStorage.setItem('role', res.role);

          observer.next(res);
          observer.complete();
        },
        error: (err) => {
          observer.error(err);
        }
      });
  });
}
}