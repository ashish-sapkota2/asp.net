import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject, take } from 'rxjs';
import { MessagesComponent } from '../messages/messages.component';
import { Message } from '../_models/message';
import { User } from '../_models/user';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = "https://localhost:7164/api/";
  hubUrl ="https://localhost:7164/hubs/";
  private hubConnection :HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();


  constructor(private http: HttpClient) { }

  createHubConnection(user:User, otherUsername:string){
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl + 'message?user=' + otherUsername,{
      accessTokenFactory: ()=>user.token
    })
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Information)
    .build()

    this.hubConnection.start()
    .then(()=>console.log('Hub Connection started'))
    .catch(error=>console.log(error));
    
    this.hubConnection.on('NewMessage',message=>{
      console.log('New message received:', message);
      this.messageThread$.pipe(take(1)).subscribe(messages=>{
        console.log('current messaged:', messages)
        this.messageThreadSource.next([...messages,message])
        console.log(messages);
      })
    })
    this.hubConnection.on('ReceiveMessageThread', messages=>{
      console.log('Received message thread:', messages);
      this.messageThreadSource.next(messages);
    });
    this.hubConnection.onclose(error => {
      console.error('Hub connection closed:', error);
      // Optionally: retry logic or user notification
    });
  }

  stopHubConnection(){
    if(this.hubConnection){

      this.hubConnection.stop();
    }
  }

  getMessages(pageNumber, pageSize,container){
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Message[]>(this.baseUrl + 'messages', params, this.http)
  }

  getMessageThread(username: string){
    return this.http.get<Message[]>(this.baseUrl + 'messages/thread/' +username);
  }

  async sendMessage(username:string, content:string){
    // return this.http.post<Message>(this.baseUrl + 'messages', {recipientUsername: username, content})
    return this.hubConnection.invoke('SendMessage', {recipientUsername: username, content})
    .catch(error=>console.log(error));
  }

  deleteMessage(id:number){
    return this.http.delete(this.baseUrl + 'messages/' +id);
  }
}
