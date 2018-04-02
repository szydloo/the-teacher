import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { MatDialogModule, MatButtonModule } from '@angular/material';

import { LoginComponent } from './login.component';
import { SignupComponent } from './signup.component';
import { ConfirmEqualValidatorDirective } from '../shared/confirm-equal-validator.directive';
import { SignupLoginResultDialog } from './signup-login-result-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
  ],
  declarations: [
    LoginComponent,
    SignupComponent,
    ConfirmEqualValidatorDirective,
    SignupLoginResultDialog
 ],
 entryComponents: [SignupLoginResultDialog]
})
export class SignupLoginModule { }
