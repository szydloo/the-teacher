import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { MatDialogModule, MatButtonModule, MatCheckboxModule } from '@angular/material';

import { LoginComponent } from './login.component';
import { SignupComponent } from './signup.component';
import { ConfirmEqualValidatorDirective } from '../shared/confirm-equal-validator.directive';
import { SignupLoginResultDialogComponent } from './signup-login-result-dialog.component';
import { LoginService } from './login.service';
import { AlertFilledFormsGuard } from './alert-filled-forms.guard';


@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    MatCheckboxModule,

  ],
  declarations: [
    LoginComponent,
    SignupComponent,
    ConfirmEqualValidatorDirective,
    SignupLoginResultDialogComponent
 ],
 entryComponents: [SignupLoginResultDialogComponent ],
 providers: [LoginService, AlertFilledFormsGuard]
})
export class SignupLoginModule { }
