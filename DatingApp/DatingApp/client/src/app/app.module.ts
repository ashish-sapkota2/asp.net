import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import {FormsModule} from '@angular/forms';
import { Component } from '@angular/core';
import { BsDropdownConfig, BsDropdownModule } from 'ngx-bootstrap/dropdown';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    
  ],
  
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BsDropdownModule.forRoot()
  
  ],
  providers: [
    provideHttpClient(withFetch()),
    BrowserAnimationsModule,
    FormsModule,
    BsDropdownConfig,
    provideAnimations()
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
export class DemoDropdownAnimatedComponent {}
