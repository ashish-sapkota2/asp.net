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

  getMember(username:string){
    return this.http.get<Member>(this.baseUrl + 'users/'+ username)
  }
  updateMember(member: Member){
    return this.http.put<Member>(this.baseUrl + 'users/', member)
  }
  setMainPhoto(photoId: number){
    return this.http.put(this.baseUrl+ 'users/set-main-photo/' + photoId, {})
  }
  deletePhoto(photoId: number){
    return this.http.delete(this.baseUrl+ 'users/delete-photo/' +photoId);
  }
}
