import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  public connection : signalR.HubConnection = new signalR.HubConnectionBuilder()
  .withUrl('https://localhost:7085/chat')
  .configureLogging(signalR.LogLevel.Information)
  .build();

  public messages$ = new BehaviorSubject<any>([]);
  public connectedUsers$ = new BehaviorSubject<string[]>([]);
  public messages : any[]=[];
  public users: string[]=[];

  constructor() { 
    this.start()
    this.connection.on("ReceiveMessage", (user:String, message:String, messageTime:String)=>{
      console.log(user,message,messageTime);
      this.messages= [...this.messages,{user,message,messageTime}]
      this.messages$.next(this.messages);
    })

    this.connection.on('ConnectedUser', (users:any)=>{
      console.log(users);
      this.connectedUsers$.next(users);
    })

  }

  //start connection 
  public async start(){
    try {
      await this.connection.start();
    } catch (error) {
      console.log(error);
    }
  }

  //join room

  public async joinRoom(user:string, room:string){
   return this.connection.invoke('JoinRoom', {user,room}) 
  }

  //send messages
public async sendMessage (message:string){
  return this.connection.invoke('SendMessage',message)
}

  //leave chat
  public async leaveChat(){
    return this.connection.stop();
  }
}
