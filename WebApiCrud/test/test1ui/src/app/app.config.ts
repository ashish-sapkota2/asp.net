import { provideHttpClient, withFetch } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';


import { routes } from './app.routes';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideHttpClient(withFetch()),
     FormsModule, CommonModule, BsDropdownConfig,
      BrowserAnimationsModule,
      provideAnimations()
    ]
};
