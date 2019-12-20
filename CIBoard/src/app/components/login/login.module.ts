import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './login.component';
import { FormsModule } from '@angular/forms';
import {AuthGuard} from './../../guard/auth.guard';
import {SharedModule} from './../share/shared/shared.module';
import { from } from 'rxjs';

const routes:Routes=[
    { path: '',component: LoginComponent},
    { path: 'login',component: LoginComponent},
   
]



@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      RouterModule.forChild(routes),
      SharedModule
    
    ],

    declarations: [LoginComponent]
})

export class LoginModule {
    constructor()
    {
        console.log("called login")
    }
 }