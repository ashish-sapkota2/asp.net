import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl= 'https://localhost:7276/api/';
  private currentUserSource = new ReplaySubject<User | null>(1);  // ReplaySubject that holds the current user or null. It's initialized with a buffer size of 1, meaning it will replay the last emitted value to new subscribers.
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }
  
  //takes the model from the nav and paste it to the login 
  login(model:any){
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response:User)=>{
        const user= response;
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model:any){
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: any)=>{
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      })
    )
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
