import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject, take } from 'rxjs';
import { Group } from '../_models/group';
import { Message } from '../_models/message';
import { User } from '../_models/user';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = "https://localhost:7164/api/";
  hubUrl = "https://localhost:7164/hubs/";
  private hubConnection: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) { }

  createHubConnection(user: User, otherUsername: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'message?user=' + otherUsername, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    // Set up listeners before starting the connection
    this.hubConnection.on('NewMessage', message => {
      console.log('New message received:', message);
      this.messageThread$.pipe(take(1)).subscribe(messages => {
        const updatedMessages = [...messages, message];
        console.log('Updated messages:', updatedMessages);
        this.messageThreadSource.next(updatedMessages);
      });
    });

    this.hubConnection.on('ReceiveMessageThread', messages => {
      console.log('Received message thread:', messages);
      this.messageThreadSource.next(messages);
    });
    this.hubConnection.on('UpdatedGroup', (group:Group)=>{
      if(group.connections.some(x=>x.username===otherUsername)){
        this.messageThread$.pipe(take(1)).subscribe(messages=>{
          messages.forEach(message=>{
            if(!message.dateRead){
              message.dateRead= new Date(Date.now())
            }
          })
          this.messageThreadSource.next([...messages]);
        })
      }
    })

    this.hubConnection.onreconnecting(error => {
      console.warn('Hub connection lost, attempting to reconnect...', error);
      // Update UI to indicate reconnection in progress
    });

    this.hubConnection.onreconnected(connectionId => {
      console.log('Hub connection reestablished:', connectionId);
      // Optionally, refresh the message thread or notify the user
    });

    this.hubConnection.onclose(error => {
      console.error('Hub connection closed:', error);
      // Optionally: retry logic or user notification
    });

    // Start the connection after all listeners are set
    this.hubConnection.start()
      .then(() => console.log('Hub Connection started'))
      .catch(error => console.log(error));
      console.log('Hub connection state:', this.hubConnection.state);
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }

  getMessages(pageNumber, pageSize, container) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Message[]>(this.baseUrl + 'messages', params, this.http);
  }

  getMessageThread(username: string) {
    return this.http.get<Message[]>(this.baseUrl + 'messages/thread/' + username);
  }

  async sendMessage(username: string, content: string) {
    try {
      if (this.hubConnection && this.hubConnection.state === 'Connected') {
        await this.hubConnection.invoke('SendMessage', { recipientUsername: username, content });
      } else {
        console.log('Cannot send message, hub connection is not active.');
      }
    } catch (error) {
      console.error('Error sending message:', error);
      // Optionally, implement retry logic here
    }
  }

  deleteMessage(id: number) {
    return this.http.delete(this.baseUrl + 'messages/' + id);
  }
}
