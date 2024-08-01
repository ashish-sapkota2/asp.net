import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getActiveConsumer } from '@angular/core/primitives/signals';
import { map, retry } from 'rxjs';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl ='https://localhost:7164/api/';
  constructor(private http: HttpClient) { }
  
  
  getMembers(userParams: UserParams){
 
    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize)
    
    params= params.append("currentUsername",userParams.username);
    params = params.append("gender", userParams.gender);
    params = params.append("MinAge", userParams.minAge.toString());
    params = params.append("MaxAge", userParams.maxAge.toString());

    
    return this.getPaginatedResult<Member[]>(this.baseUrl+ 'users',params);
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

  private getPaginatedResult<T>(url,params) {
  const  paginatedResult: PaginatedResult<T>= new PaginatedResult<T>();
    
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        const paginatedheader = response.headers.get('Pagination');
        paginatedResult.pagination = JSON.parse(paginatedheader);
        var pagination = response.headers.get('Pagination');
        if (pagination == null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('headers'));

        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(pageNumber: number, pageSize:number){
    let params = new HttpParams();

      params= params.append('pageNumber', pageNumber.toString());
      params= params.append('pageSize', pageSize.toString());
      
      return params;

  }


}
