import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, } from '@angular/forms';
import { CommonModule, NgFor } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { HomeComponent } from './home/home.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, CommonModule,
     HttpClientModule, NgFor, NavComponent,
    HomeComponent
    ],
  templateUrl: './app.component.html',
  styleUrls:[ './app.component.css',

]
})
export class AppComponent {
  title = 'Dating App';
  users: any;

  constructor(private http: HttpClient , private accountService: AccountService){
    this.setCurrentUser();
  }
setCurrentUser(){
  var result = localStorage.getItem('user');
  if(result){

    const user: User= JSON.parse(result);
    this.accountService.setCurrentUser(user);
  }else{
    console.log("Error Occurs")
  }
}
}
