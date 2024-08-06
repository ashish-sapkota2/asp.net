import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getActiveConsumer } from '@angular/core/primitives/signals';
import { map, retry, take } from 'rxjs';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  user:User;
  userparams: UserParams;
  baseUrl ='https://localhost:7164/api/';
  constructor(private http: HttpClient, private accountService : AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>{
      this.user=user;
      this.userparams= new UserParams(user);
    })
  }

  getUserParams(){
    return this.userparams
  }
  setUserParams(params:UserParams){
    this.userparams=params;
  }
  
  resetUserParams(){
    this.userparams= new UserParams(this.user);
    return this.userparams;
  }
  
  getMembers(userParams: UserParams){
 
    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize)
    
    //  params= params.append("currentUsername",userParams.username);
    params = params.append("gender", userParams.gender);
    params = params.append("MinAge", userParams.minAge.toString());
    params = params.append("MaxAge", userParams.maxAge.toString());
    params= params.append("orderBy", userParams.orderBy);

    
    return getPaginatedResult<Member[]>(this.baseUrl+ 'users',params, this.http);
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

  addLike(username:string){
    return this.http.post(this.baseUrl + 'likes/'+ username,{})
  }

  getLikes(predicate: string, pageNumber, pageSize){
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicate)
    return getPaginatedResult<Partial<Member[]>>(this.baseUrl + 'likes', params, this.http);
  }




}
