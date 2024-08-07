import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, mapToCanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, take } from 'rxjs';
import { AccountService } from '../_services/account.service';



export const authGuard: CanActivateFn = (
  route:ActivatedRouteSnapshot, 
  state: RouterStateSnapshot,
  ) => {

    const accountService= inject(AccountService) as AccountService;
    const toastrService = inject(ToastrService) as ToastrService;
    const router = inject(Router) as Router;
    
    return accountService.currentUser$.pipe(
      take(1),
      map(user=>{
       if(user){
         return true;
        }else{
          toastrService.error('You need to login');
         router.navigate(['/']);
         return false;
       }
      })
    )
  };



