import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JsonPipe } from '@angular/common';


import { confirmEqualPasswordValidator, confirmEqualEmailValidator } from '../shared/confirm-equal.validator'
import { throws } from 'assert';
import { UserService } from '../user/user.service';
import { User } from '../models/user';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
    pageTitle: string = "Sign Up!"
    signUpForm: FormGroup;


    constructor(private fb: FormBuilder, private userService: UserService) { }

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
        let u: User = new User();
        u.email = this.signUpForm.value.emailGroup.email;
        u.password = this.signUpForm.value.passwordGroup.password;
        u.username = this.signUpForm.value.username;
        u.role = 'user';
        console.log(JSON.stringify(u));
        this.userService.saveUser(u).subscribe(() => this.onSaveComplete,
                                                (err) => console.log(err));
    }

    onSaveComplete() {
        this.signUpForm.reset();
    }


    //#region  Validation errors display functions go here
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
