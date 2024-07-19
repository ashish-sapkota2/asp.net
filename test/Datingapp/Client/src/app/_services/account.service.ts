import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl ="https://localhost:7164/api/";

  constructor(private http: HttpClient) { }

  login(model:any){
    return this.http.post(this.baseUrl+ 'account/login', model).pipe(
      map((response:any)=>{
        const user = response;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
        }
      })
    )
  }
  logout(){
    localStorage.removeItem('user');
  }
}
