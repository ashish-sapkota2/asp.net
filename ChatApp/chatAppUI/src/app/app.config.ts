import { ApplicationConfig } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),FormBuilder]
};
