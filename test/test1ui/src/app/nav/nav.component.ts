import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import {BsDropdownConfig, BsDropdownModule} from 'ngx-bootstrap/dropdown'
import { User } from '../_models/user';
import { Observable } from 'rxjs';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { provideToastr, ToastrService } from 'ngx-toastr';
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from '../app.component';

bootstrapApplication(AppComponent, {
  providers: [
    provideToastr({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }), 
  ]
});
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, CommonModule, BsDropdownModule, RouterLink],
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css',],})

export class NavComponent {
  model:any={}
  
  constructor(public accountService: AccountService, private router: Router, 
    private toastr: ToastrService){

  }

login(){
  this.accountService.login(this.model).subscribe(response=>{
    this.router.navigateByUrl('/members');
    this.toastr.success("LoggedIn Successfully");
  },error=>{
    this.toastr.error(error.error?.message);
  })

  console.log(this.model)
}
logout(){
  this.accountService.logout()
  this.router.navigateByUrl('/')

}

}
