import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { Router, RouterLink, UrlSerializer } from '@angular/router';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { HomeComponent } from '../home/home.component';
import { HasRoleDirective } from '../_directives/has-role.directive';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [BsDropdownModule, FormsModule, CommonModule,
     HomeComponent, RouterLink, HasRoleDirective],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  model: any={}

  
  constructor(public accountService: AccountService, private router:Router,
     private toastr : ToastrService){

  }
  login(){
    this.accountService.login(this.model).subscribe(response=>{
      this.router.navigateByUrl('/members');
      this.toastr.success("LoggedIn Successfully");
      console.log(response);
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/')

  }

}
