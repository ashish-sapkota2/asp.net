import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl ="https://localhost:7164/hub/";

  private hubConnection: HubConnection;

  constructor(private toastr: ToastrService) { }

  createHubConnection(user:User){
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl + 'presence',{
      accessTokenFactory: ()=>user.token
    })
    .withAutomaticReconnect()
    .build()

    this.hubConnection.start()
    .then(()=>console.log('connected to signalR'))
    .catch(error=> console.log(error));
    
    this.hubConnection.on('UserIsOnline', username=>{
      this.toastr.info(username + ' is connected');
    })

    this.hubConnection.on('UserIsOffline', username=>{
      this.toastr.warning(username + 'has disconnected');
    })
  }

  stopHubConnection(){
    this.hubConnection.stop().catch(error=>console.log(error));
  }
}
