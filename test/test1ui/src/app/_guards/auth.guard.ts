import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { inject } from '@angular/core';
import { map, take } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot, 
  state: RouterStateSnapshot
) => {
  const accountService = inject(AccountService);
  const toastrService = inject(ToastrService);
  const router = inject(Router);

  return accountService.currentUser$.pipe(
    take(1),
    map(user => {
      if (user) {
        return true;
      } else {
        toastrService.error("You need to log in");
        router.navigate(['/login']);
        return false;
      }
    })
  );
};
