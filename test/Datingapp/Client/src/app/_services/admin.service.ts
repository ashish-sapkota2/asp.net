import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
baseUrl='https://localhost:7164/api/';

  constructor(private http: HttpClient) { }

  getUsersWithRoles(){
    return this.http.get<Partial<User>>(this.baseUrl + 'admin/users-with-roles');
  }
}
