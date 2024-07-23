import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../_models/member';



@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl= 'https://localhost:7276/api/';

  constructor(private http: HttpClient) { }

  private getHttpOptions(){

    const user = localStorage.getItem('user');
    let token =''
    if(user){
      token =JSON.parse(user);
    }
    return{
      headers: new HttpHeaders({
        
        Authorization: 'Bearer ' +token
      })
    }
  }
  getMembers(){
    return this.http.get<Member[]>(this.baseUrl + 'users',this.getHttpOptions())
  }

  getMember(username:string){
    return this.http.get<Member>(this.baseUrl + 'users/'+ username, this.getHttpOptions())
  }

}
