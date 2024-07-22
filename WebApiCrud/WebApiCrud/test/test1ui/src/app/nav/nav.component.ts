import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import {BsDropdownConfig, BsDropdownModule} from 'ngx-bootstrap/dropdown'
import { User } from '../_models/user';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, CommonModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css',

]
})
export class NavComponent {
  model:any={}
  
  constructor(public accountService: AccountService){

  }

login(){
  this.accountService.login(this.model).subscribe(response=>{
    console.log(response);
  })

  console.log(this.model)
}
logout(){
  this.accountService.logout()

}

}
