import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JsonPipe } from '@angular/common';
import { MatDialog } from '@angular/material';

import { confirmEqualPasswordValidator, confirmEqualEmailValidator } from '../shared/confirm-equal.validator';
import { throws } from 'assert';
import { UserService } from '../user/user.service';
import { SignupLoginResultDialogComponent } from './signup-login-result-dialog.component';
import { ServiceErrorCodes } from '../exception/service-error-codes';
import { RegisterUserCommand } from '../models/commands/user/register-user';
import { Router } from '@angular/router';
import { LoginService } from './login.service';
import { LoginUserCommand } from '../models/commands/user/login-user-command';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
    pageTitle = 'Sign Up!';
    signUpForm: FormGroup;


    constructor(private fb: FormBuilder, private userService: UserService, private loginService: LoginService,
        private dialog: MatDialog, private router: Router) { }

    ngOnInit() {
        // TODO change initial data
        this.signUpForm = this.fb.group({
            username: ['testUsername', [Validators.required]],
            agreementOne: [true, [Validators.requiredTrue]],
            emailGroup: this.fb.group({
                email: ['testUsername@tet.com', [Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+')]],
                confirmEmail: ['testUsername@tet.com', [Validators.required]],
            }, { validator: confirmEqualEmailValidator }),
            passwordGroup: this.fb.group({
                password: ['testSecret', [Validators.required]],
                confirmPassword: ['testSecret', [Validators.required]],
            }, { validator: confirmEqualPasswordValidator }),

        });
    }

    saveUser() {
        let u: RegisterUserCommand = new RegisterUserCommand();
        Object.assign(u ,{ username : this.signUpForm.controls.username.value },
             {email : this.signUpForm.controls.emailGroup.value.email}, { password : this.signUpForm.controls.passwordGroup.value.password })

        this.userService.saveUser(u).subscribe(() => this.onSaveComplete(),
            (err) => this.handleErrorWithModalPopup(err))
    }

    onSaveComplete() {
        this.signUpForm.reset();
        this.router.navigateByUrl('home');
    }


    // Handle error sent by api
    handleErrorWithModalPopup(err: any): void {
        if (err.error.code === ServiceErrorCodes.emailInUse) {
            console.log(JSON.stringify(err.error.message));
            const dialogRef = this.dialog.open(SignupLoginResultDialogComponent, {
                width: '500px',
                height: '200px',
                data: { errorMessage: err.error.message }
            });
            dialogRef.afterClosed().subscribe(() => {
                this.signUpForm.controls.emailGroup.reset();
            });
        } else {
            // If Not custom error log into database?
            console.error(err);
        }
    }

    //#region  Validation errors display functions go here
    addIsInvalidIfErrors(controlName: string) {
        const styles = {
            'is-invalid': this.hasErrors(controlName),
        };
        return styles;
    }

    addTextDangerIfErrors(controlName: string) {
        const styles = {
            'text-danger': this.hasErrors(controlName),
        };
        return styles;
    }

    addAlertDangerIfNotMatching(controlGroupName: string, confirmationFieldName: string) {
        const controlGroup = this.signUpForm.get(controlGroupName);
        const confirmationControl = this.signUpForm.get(confirmationFieldName);
        if (controlGroup.errors === null) { return; }
        const styles = { 'alert alert-danger': this.signUpForm.get(controlGroupName).errors.notEqual && confirmationControl.touched };
        return styles;
    }

    hasErrors(controlName: string): boolean {
        const control = this.signUpForm.get(controlName);
        if (control === null || control === undefined) { throw new Error(`Lacking control with name ${controlName}.`); }
        if (control.touched && control.errors !== null) {
            return true;
        } else {
            return false;
        }
    }

    isRequired(controlName: string): boolean {
        const control = this.signUpForm.get(controlName);
        if (control === null || control === undefined) { throw new Error(`Lacking control with name ${controlName}.`); }
        if (control.errors === null) { return false; }
        return (control.touched && (control.errors.required));
    }

    isMatchingEmailPattern(controlName: string): boolean {
        const control = this.signUpForm.get(controlName);
        if (control === null || control === undefined) { throw new Error(`Lacking control with name ${controlName}.`); }
        if (control.errors === null) { return false; }
        return (control.touched && (control.errors.pattern));
    }

    //#endregion
}
