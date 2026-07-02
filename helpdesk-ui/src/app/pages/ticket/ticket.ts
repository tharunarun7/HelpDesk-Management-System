import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketService } from '../../services/ticket.service';
import { Ticket } from '../../models/ticket';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-ticket',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './ticket.html',
  styleUrl: './ticket.css'
})
export class Tickets implements OnInit {

  tickets: Ticket[] = [];

  role: string = '';

  // NEW
  username: string = '';

  ticketForm!: FormGroup;

  selectedTicketId: number = 0;
  selectedTicket: Ticket | null = null;

showModal = false;

  selectedFile!: File;

  imagePreview: any;

  // Dashboard Counts
  openCount = 0;
  inProgressCount = 0;
  closedCount = 0;
  totalTickets = 0;
  // Search & Filter
searchText = '';

selectedStatus = 'All';

selectedPriority = 'All';

  constructor(
    private ticketService: TicketService,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {

    this.ticketForm = this.fb.group({
  title: ['', Validators.required],
  description: ['', Validators.required],
  status: ['Open', Validators.required],

  // NEW
  priority: ['Medium', Validators.required],

  createdBy: [Number(localStorage.getItem('userId'))],
  assignedTo: [1],
  screenshotPath: ['']
});

  }

  ngOnInit(): void {

    this.role = localStorage.getItem('role') || '';

    // NEW
    this.username = localStorage.getItem('username') || '';

    this.loadTickets();

  }

  onFileSelected(event: any): void {

    if (!event.target.files.length) {
      return;
    }

    this.selectedFile = event.target.files[0];

    const reader = new FileReader();

    reader.onload = () => {

      this.imagePreview = reader.result;

    };

    reader.readAsDataURL(this.selectedFile);

    this.ticketService.uploadScreenshot(this.selectedFile)
      .subscribe({

        next: (response: any) => {

          this.ticketForm.patchValue({

            screenshotPath: response.fileName

          });

        },

        error: (err) => console.log(err)

      });

  }

 editTicket(ticket: Ticket): void {

  this.selectedTicketId = ticket.id;

  this.ticketForm.patchValue({

    title: ticket.title,
    description: ticket.description,
    status: ticket.status,

    // NEW
    priority: ticket.priority,

    createdBy: ticket.createdBy,
    assignedTo: ticket.assignedTo,
    screenshotPath: ticket.screenshotPath

  });

}
openTicket(ticket: Ticket): void {

  this.selectedTicket = ticket;

  this.selectedTicketId = ticket.id;

  this.showModal = true;

  this.ticketForm.patchValue({

    title: ticket.title,
    description: ticket.description,
    status: ticket.status,
    priority: ticket.priority,
    createdBy: ticket.createdBy,
    assignedTo: ticket.assignedTo,
    screenshotPath: ticket.screenshotPath

  });

}
closeModal(): void {

  this.showModal = false;

  this.selectedTicket = null;

  this.selectedTicketId = 0;

}

  onSubmit(): void {

    const ticket: Ticket = this.ticketForm.value;

    if (this.role === 'Admin') {

  ticket.id = this.selectedTicketId;

  this.ticketService.updateTicket(ticket)
    .subscribe({

      next: () => {

        alert('Ticket Updated Successfully');

        // Reload table
        this.loadTickets();

        // Close popup
        this.closeModal();

        // Reset form
        this.ticketForm.reset({
          title: '',
          description: '',
          status: 'Open',
          priority: 'Medium',
          createdBy: Number(localStorage.getItem('userId')),
          assignedTo: 1,
          screenshotPath: ''
        });

      },

      error: err => console.log(err)

    });

  return;

}
    this.ticketService.createTicket(ticket)
      .subscribe({

        next: () => {

          alert('Ticket Created Successfully');

          this.ticketForm.reset({

  title: '',
  description: '',
  status: 'Open',
  priority: 'Medium',
  createdBy: Number(localStorage.getItem('userId')),
  assignedTo: 1,
  screenshotPath: ''

});

          this.imagePreview = null;

          this.loadTickets();

        },

        error: err => console.log(err)

      });

  }

  loadTickets(): void {

  this.ticketService.getAllTickets()
    .subscribe({

      next: (data) => {

        // Sort so Closed tickets appear at the bottom
        this.tickets = data.sort((a, b) => {

          if (a.status === 'Closed' && b.status !== 'Closed')
            return 1;

          if (a.status !== 'Closed' && b.status === 'Closed')
            return -1;

          return 0;

        });

        this.openCount =
          this.tickets.filter(t => t.status === 'Open').length;

        this.inProgressCount =
          this.tickets.filter(t => t.status === 'In Progress').length;

        this.closedCount =
          this.tickets.filter(t => t.status === 'Closed').length;

        this.totalTickets = this.tickets.length;

        this.cdr.detectChanges();

      },

      error: err => console.log(err)

    });

}
filterByStatus(status: string): void {

  this.selectedStatus = status;

}
get filteredTickets(): Ticket[] {

  return this.tickets.filter(ticket => {

    const matchesSearch =
      this.searchText === '' ||
      ticket.title.toLowerCase().includes(this.searchText.toLowerCase()) ||
      ticket.description.toLowerCase().includes(this.searchText.toLowerCase());

    const matchesStatus =
      this.selectedStatus === 'All' ||
      ticket.status === this.selectedStatus;

    const matchesPriority =
      this.selectedPriority === 'All' ||
      ticket.priority === this.selectedPriority;

    return matchesSearch && matchesStatus && matchesPriority;

  });

}

  logout(): void {

    localStorage.clear();

    window.location.reload();

  }

}


