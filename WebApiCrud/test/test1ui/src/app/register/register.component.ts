import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  @Output() cancelRegister= new EventEmitter();
  model: any={};

  constructor(private accountService: AccountService, private toastr : ToastrService){

  }

  register(){
    this.accountService.register(this.model).subscribe(response=>{
      console.log(response);
      this.cancel();

    },error=>{
      this.toastr.error(error)
    })
  }
  cancel(){
    console.log("cancelled");
    this.cancelRegister.emit(false);
  }
}
