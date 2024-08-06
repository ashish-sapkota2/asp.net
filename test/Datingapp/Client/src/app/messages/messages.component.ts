import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TimeagoModule } from 'ngx-timeago';
import { Message } from '../_models/message';
import { Pagination } from '../_models/pagination';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messages',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink,TimeagoModule, PaginationModule],
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessagesComponent {
messages: Message[];
pagination: Pagination;
container= 'Unread';
pageNumber=1;
pageSize=5;

constructor(private messageService: MessageService){
  this.loadMessages();
}

loadMessages(){
  this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe(response=>{
    this.messages = response.result;
    this.pagination= response.pagination;
    console.log(this.container);
  })
}
pageChanged(event: any){
  this.pageNumber= event.page;
  this.loadMessages();
}
}
