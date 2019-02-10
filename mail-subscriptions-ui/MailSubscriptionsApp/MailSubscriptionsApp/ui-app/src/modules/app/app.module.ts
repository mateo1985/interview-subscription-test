import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedMaterialModule } from '../shared-material/shared-material.module';
import { HomeComponent } from './components/home/home.component';
import { SubscriptionsComponent } from './components/subscriptions/subscriptions.component';
import { ErrorComponent } from './components/error/error.component';
import { AppRouterModule } from '../app-router/app-router.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher, ErrorStateMatcher } from '@angular/material';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SubscriptionsComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    SharedMaterialModule,
    AppRouterModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ToastrModule.forRoot()
  ],
  providers: [
    {provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
