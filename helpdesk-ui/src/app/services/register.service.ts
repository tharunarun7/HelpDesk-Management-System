import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Register } from '../models/register';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl = 'https://localhost:7193/api/Auth';

  constructor(private http: HttpClient) { }

  register(user: Register): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user);
  }
}