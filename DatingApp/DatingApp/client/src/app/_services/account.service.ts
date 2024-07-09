import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { User } from '../models/user';


//services are injectable
@Injectable({
  providedIn: 'root'
})

//data stored inside the service doesnot get destroyed until our application is  closed down
//where are component are destroyed as soon as the are not in use 

export class AccountService {
   baseUrl ='https://localhost:7055/api/';
   private currentUserSource = new ReplaySubject<User| null>(1);
   currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any ){
    return this.http.post<User>(this.baseUrl+ 'account/login', model).pipe(
      map((response: User)=>{
        const user= response;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
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
