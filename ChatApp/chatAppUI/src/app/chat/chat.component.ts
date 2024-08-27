import { CommonModule } from '@angular/common';
import { AfterViewChecked, Component, EventEmitter, inject, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ChatService } from '../chat.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit{


  chatService = inject(ChatService);
  loggedInUserName = sessionStorage.getItem("user");
  roomName = sessionStorage.getItem('room');
  router = inject(Router);
  inputMessage ="";
  messages:any[]=[];
  @ViewChild('scrollMe') private scrollConteroller = EventEmitter;

  ngOnInit(): void {
    this.chatService.messages$.subscribe(res=>{
      this.messages=res;
      console.log(this.messages)
    })
  }

sendMessage(){
  this.chatService.sendMessage(this.inputMessage)
  .then(()=>{
    this.inputMessage=''
  }).catch((err)=>{
    console.log(err);
  })
}

leaveChat(){
  this.chatService.leaveChat()
  .then(()=>{
    this.router.navigate(['welcome'])
  }).catch((err)=>{
    console.log(err);
    
  })
}
}
