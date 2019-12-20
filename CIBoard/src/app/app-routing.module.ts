import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Router } from '@angular/router';


const appRoutes: Routes = [  
  { path: '**', loadChildren: './../app/components/login/login.module#LoginModule' },
  { path: '', loadChildren: './../app/components/login/login.module#LoginModule' },  
];

@NgModule({
  imports: [Router],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents:ModuleWithProviders=RouterModule.forRoot(appRoutes)