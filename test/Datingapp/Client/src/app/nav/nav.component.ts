import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { AccountService } from '../_services/account.service';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [BsDropdownModule, FormsModule, CommonModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  model: any={}
  loggedIn: boolean | undefined;
  
  constructor(private accountService: AccountService){}
  login(){
    this.accountService.login(this.model).subscribe(response=>{
      console.log(response);
      this.loggedIn= true;
    })
  }

  logout(){
    this.loggedIn=false;
  }
}
