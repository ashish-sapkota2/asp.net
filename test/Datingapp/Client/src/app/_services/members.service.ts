import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getActiveConsumer } from '@angular/core/primitives/signals';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl ='https://localhost:7164/api/';
  
  constructor(private http: HttpClient) { }
  
 
  getMembers(){
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }
}
