import { CommonModule } from '@angular/common';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { provideRouter } from '@angular/router';
import {BrowserAnimationsModule, provideAnimations} from '@angular/platform-browser/animations'

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideHttpClient(withFetch()), 
    CommonModule, FormsModule, BrowserAnimationsModule, provideAnimations()]
};
