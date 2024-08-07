import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl ="https://localhost:7164/api/";
 //Replaysubject store the value and anytime subscriber subscribe the observable it will emit last value stored
  private currentUserSource = new ReplaySubject<User| null>(1)

  //as this is observable by convention it uses dollar sign
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model:any){
    return this.http.post<User>(this.baseUrl+ 'account/login', model).pipe(
      map((response:User)=>{
        const user = response;
        if(user){
          this.setCurrentUser(user)
          // localStorage.setItem('user',JSON.stringify(user));
          // this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model:any){
    return this.http.post(this.baseUrl +'account/register', model).pipe(
      map((user:any)=>{
        if(user){
        //   localStorage.setItem('user',JSON.stringify(user));
        //   this.currentUserSource.next(user);
        this.setCurrentUser(user)
        }
        return user;
      })
    )
  }

  setCurrentUser(user:User){
    user.roles=[];
    const roles =this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles=roles: user.roles.push(roles);
    localStorage.setItem('user',JSON.stringify(user));
    this.currentUserSource.next(user);
  }
  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  getDecodedToken(token){
    //atob decode the information inside token
    return JSON.parse(atob(token.split('.')[1]));
  }
}
