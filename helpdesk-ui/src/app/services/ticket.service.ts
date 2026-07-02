import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket } from '../models/ticket';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  private apiUrl = 'https://localhost:7193/api/Ticket';

  constructor(private http: HttpClient) { }

  getAllTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.apiUrl);
  }

  getTicketById(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/${id}`);
  }

  createTicket(ticket: Ticket): Observable<any> {
    return this.http.post(this.apiUrl, ticket);
  }

  updateTicket(ticket: Ticket): Observable<any> {
    return this.http.put(`${this.apiUrl}/${ticket.id}`, ticket);
  }

  deleteTicket(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  uploadScreenshot(file: File): Observable<any> {

    const formData = new FormData();

    formData.append('file', file);

    return this.http.post(
      `${this.apiUrl}/UploadScreenshot`,
      formData
    );
  }

}