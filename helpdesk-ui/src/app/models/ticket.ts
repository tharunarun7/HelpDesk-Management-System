export interface Ticket {

  id: number;

  title: string;

  description: string;

  status: string;

  priority: string;
  adminResponse?: string;
  responseDate?:string;

  createdBy: number;

  createdByUsername: string;

  assignedTo: number;

  screenshotPath?: string;

  createdDate: string;

}