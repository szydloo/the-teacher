import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JsonPipe } from '@angular/common';


import { confirmEqualPasswordValidator, confirmEqualEmailValidator } from '../shared/confirm-equal.validator'
import { throws } from 'assert';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
    pageTitle: string = "Sign Up!"
    signUpForm: FormGroup;

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.signUpForm = this.fb.group({
            username: ['', [Validators.required]],
            agreementOne: [false, [Validators.requiredTrue]],
            emailGroup: this.fb.group({
                email: ['', [Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+')]],
                confirmEmail: ['', [Validators.required]],
            }, { validator: confirmEqualEmailValidator }),
            passwordGroup: this.fb.group({
                password: ['', [Validators.required]],
                confirmPassword: ['', [Validators.required]],
            }, { validator: confirmEqualPasswordValidator }),
           
        });
    }


    //#region  Validation errors display functions
    addIsInvalidIfErrors(controlName: string) {
        let styles = {
            'is-invalid': this.hasErrors(controlName),
        }
        return styles;
    }

    addTextDangerIfErrors(controlName: string) {
        let styles = {
            'text-danger': this.hasErrors(controlName),
        }
        return styles;
    }

    addAlertDangerIfNotMatching(controlGroupName: string, confirmationFieldName: string) {
        let controlGroup = this.signUpForm.get(controlGroupName);
        let confirmationControl = this.signUpForm.get(confirmationFieldName);
        if(controlGroup.errors === null) return;
        let styles = { 'alert alert-danger': this.signUpForm.get(controlGroupName).errors.notEqual && confirmationControl.touched }
        return styles;
    }

    hasErrors(controlName: string): boolean {
        let control = this.signUpForm.get(controlName);
        if (control === null || control === undefined) throw `Lacking control with name ${controlName}.`;
        if (control.touched && control.errors !== null) {
            return true;
        } else {
            return false;
        }
    }

    isRequired(controlName: string): boolean {
        let control = this.signUpForm.get(controlName);
        if (control === null || control === undefined) throw `Lacking control with name ${controlName}.`;
        if (control.errors === null) return false;
        return (control.touched && (control.errors.required));
    }

    isMatchingEmailPattern(controlName: string): boolean {
        let control = this.signUpForm.get(controlName);
        if (control === null || control === undefined) throw `Lacking control with name ${controlName}.`;
        if (control.errors === null) return false;
        return (control.touched && (control.errors.pattern));
    }

    //#endregion
}
