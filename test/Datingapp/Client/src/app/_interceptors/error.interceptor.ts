import { HttpInterceptorFn, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = 
(req: HttpRequest<unknown>, next)=> {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error) {
        switch (error.status) {
         
          case 400:
            if (error.error.errors) {
              const modalStateErrors = [];
              for (const key in error.error.errors) {
                if (error.error.errors[key]) {
                  modalStateErrors.push(error.error.errors[key])
                }
              }
              throw modalStateErrors.flat();
            } else if (typeof(error.error) === 'object') {
              toastr.error(error.error.message);
            } else {
              toastr.error(error.error);
            }
            break;
          case 401:
            toastr.error(error.statusText, 'Unauthorized'); // Use 'Unauthorized' or other string for the title
            break;
          case 404:
            router.navigateByUrl('/not-found');
            break;
          case 500:
            const navigationExtras: NavigationExtras = { state: { error: error.error } };
            router.navigateByUrl('/server-error', navigationExtras);
            break;
          default:
            toastr.error('Something unexpected went wrong', 'Error'); // Use 'Error' or other string for the title
            console.error(error);
            break;
        }
      }
      return throwError(() => error); // Properly propagate the error
    })
  );
};
