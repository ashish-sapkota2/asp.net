import { CommonModule, NgFor } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, CommonModule,
     NavComponent,HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Client';
  users: any;
  
  constructor(private http: HttpClient, private accountService: AccountService ){
    this.getUsers();
    this.serCurrentUser();
  }

  serCurrentUser(){
    var response =localStorage.getItem('user')
    if(response){

      const user: User = JSON.parse(response);
      this.accountService.setCurrentUser(user);
    }
  }
  getUsers(){

    this.http.get('https://localhost:7164/api/users').subscribe(response=>{
      this.users= response;
    })
  }

}
