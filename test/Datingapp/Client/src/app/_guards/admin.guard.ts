import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';
import { AccountService } from '../_services/account.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService= inject(AccountService) as AccountService;
  const toastrService = inject(ToastrService) as ToastrService;

  return accountService.currentUser$.pipe(
    map(user=>{
      if(user.roles.includes('Admin') || user.roles.includes('Moderator')){
        return true;
      }
      toastrService.error("You cannot enter this area");
      return false;
    })
  );
};
