import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  standalone: true,
  imports: [],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.css'
})
export class TestErrorsComponent {

  baseUrl ='https://localhost:7276/api/';

  constructor(private http : HttpClient){

  }

  get404Error(){
    this.http.get(this.baseUrl+'buggy/not-found').subscribe(response=>{
      console.log(response);
    })
  }
  get400Error(){
    this.http.get(this.baseUrl+'buggy/bad-request').subscribe(response=>{
      console.log(response);
    })
  }
  get500Error(){
    this.http.get(this.baseUrl+'buggy/server-error').subscribe(response=>{
      console.log(response);
    })
  }
  get401Error(){
    this.http.get(this.baseUrl+'buggy/auth').subscribe(response=>{
      console.log(response);
    })
  }
  get400ValidationError(){
    this.http.post(this.baseUrl+'account/register',{}).subscribe(response=>{
      console.log(response);
    })
  }

}
