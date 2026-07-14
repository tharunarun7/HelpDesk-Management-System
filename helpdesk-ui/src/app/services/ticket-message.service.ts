import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { TicketMessage } from '../models/ticket-message';
import { SendMessage } from '../models/send-message';

@Injectable({
  providedIn: 'root'
})
export class TicketMessageService {

  private apiUrl = 'https://localhost:7193/api/TicketMessage';

  constructor(private http: HttpClient) { }

  getMessages(ticketId: number): Observable<TicketMessage[]> {

    return this.http.get<TicketMessage[]>(`${this.apiUrl}/${ticketId}`);

  }

  sendMessage(message: SendMessage): Observable<any> {

    return this.http.post(this.apiUrl, message);

  }

}