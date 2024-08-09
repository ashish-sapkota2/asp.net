import { CommonModule } from '@angular/common';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { provideRouter } from '@angular/router';
import {BrowserAnimationsModule, provideAnimations} from '@angular/platform-browser/animations'

import { routes } from './app.routes';
import { provideToastr } from 'ngx-toastr';
import { jwtInterceptor } from './_interceptors/jwt.interceptor';
import { TimeagoModule } from 'ngx-timeago';
import { HasRoleDirective } from './_directives/has-role.directive';
import { ModalModule } from 'ngx-bootstrap/modal';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideHttpClient(withFetch()),
    FormsModule, BrowserAnimationsModule, provideAnimations(),
  provideToastr(), provideHttpClient(withInterceptors([jwtInterceptor])),
  importProvidersFrom(TimeagoModule.forRoot()), HasRoleDirective,importProvidersFrom(ModalModule.forRoot())
  ],
  
};
