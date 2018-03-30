import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { LoginComponent } from './login.component';
import { SignupComponent } from './signup.component';
import { ConfirmEqualValidatorDirective } from '../shared/confirm-equal-validator.directive';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    ReactiveFormsModule,
    FormsModule
  ],
  declarations: [
    LoginComponent,
    SignupComponent,
    ConfirmEqualValidatorDirective
]
})
export class SignupLoginModule { }
