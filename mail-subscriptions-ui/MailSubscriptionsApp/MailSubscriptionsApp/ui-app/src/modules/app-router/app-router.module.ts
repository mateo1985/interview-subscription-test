import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from '../app/components/error/error.component';
import { HomeComponent } from '../app/components/home/home.component';
import { SubscriptionsComponent } from '../app/components/subscriptions/subscriptions.component';

let routs: Routes = [
  { path: '', component : HomeComponent},
  { path: 'subscriptions', component: SubscriptionsComponent },
  { path: '**', component: ErrorComponent}

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routs),
    CommonModule
  ],
  exports: [
    RouterModule
  ]
})
export class AppRouterModule { }
