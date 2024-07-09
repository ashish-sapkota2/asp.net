import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './models/user';

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
    var result = localStorage.getItem('user');
    if(result){

      const users : User = JSON.parse(result);
      this.accountService.setCurrentUser(users);
    }else{
      console.log("Some Error occurs");
    }
  }
  

  getUsers() {
    this.http.get('https://localhost:7055/api/users').subscribe(response => {
      this.user = response;

   })
  }
}
