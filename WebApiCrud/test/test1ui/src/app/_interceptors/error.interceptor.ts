import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastrService = inject(ToastrService);
  const route = inject(Router);
  
  return next(req).pipe(
    catchError((error)=>{
      if(error){
        switch(error.status){
          case 400:
            if(error.error.errors){
              const modalStateErrors =[];
              for(const key in error.error.errors){
                if(error.error.errors[key]){
                  modalStateErrors.push(error.error.errors[key])
                }
              }
             throw modalStateErrors;
            }else{
              toastrService.error(error.statusText, error.status);
            }
            break;
            case 401:
              toastrService.error(error.statusText, error.status);
              break;
            case 404:
              route.navigateByUrl('/not-found');
              break;
            case 500:
              const navigationExtras : NavigationExtras ={state:{error:error.error}};
              route.navigateByUrl('/server-error', navigationExtras);
              break;

            default: 
              toastrService.error('something unexpected went wrong');
              console.log(error);
            break;
        }
      }
      return throwError(()=> new Error(error));
    })
  )
};
