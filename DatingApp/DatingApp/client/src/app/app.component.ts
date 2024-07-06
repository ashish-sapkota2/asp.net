import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  user: any;

  constructor(private http: HttpClient, private accountService: AccountService) { }
  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const users : User = JSON.parse(localStorage.getItem('user'))
  }
  

  getUsers() {
    this.http.get('https://localhost:7055/api/users').subscribe(response => {
      this.user = response;

   })
  }
}
