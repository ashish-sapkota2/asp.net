import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  registerMode =false;
  

  constructor(private http: HttpClient){

  }

  registerToggle(){
    this.registerMode =!this.registerMode;
  }
  cancelRegisterMode(event: boolean)
  {
    this.registerMode= event;
  }
}
