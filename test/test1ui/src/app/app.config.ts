import { HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';


import { routes } from './app.routes';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { provideToastr } from 'ngx-toastr';
import { errorInterceptor } from './_interceptors/error.interceptor';
import { jwtInterceptor } from './_interceptors/jwt.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideHttpClient(withFetch()),
     FormsModule, CommonModule, BsDropdownConfig,
      BrowserAnimationsModule,
      provideAnimations(),
      provideToastr(),
    provideHttpClient(withInterceptors([errorInterceptor,jwtInterceptor]),
    
    ),

    ]
};
