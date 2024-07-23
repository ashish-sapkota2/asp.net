import { HttpInterceptorFn } from '@angular/common/http';
import { Inject, inject } from '@angular/core';
import { take } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  
  let currentUser: User|null;
  inject(AccountService).currentUser$.pipe(take(1)).subscribe((user)=>{
    currentUser=user
  // })
  // accountService.currentUser$.pipe(take(1)).subscribe((user: User)=> {currentUser=user
  
    if(currentUser){
      req=req.clone({
        setHeaders:{
          Authorization: `Bearer ${currentUser.token}`
        }
      })
    }
  });
  return next(req);
};
