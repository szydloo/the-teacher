import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
    pageTitle: string = "Sign Up!"
    signUpForm: FormGroup;
    trueb: boolean = true;

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.signUpForm = this.fb.group({
            username: ['', [Validators.required]],
            emailGroup: this.fb.group({
                email: ['', [Validators.required,]],
                confirmEmail: ['', [Validators.required]],
            }),
            passwordGroup: this.fb.group({
                password: ['', [Validators.required]],
                confirmPassword: ['', [Validators.required]],
            }),
            agreementOne: [false]
        });
    }

    addIsInvalidIfErrors(controlName: string) {
        let styles = { 
            'is-invalid': this.isInvalid(controlName),
        }
        return styles;
    }

    addTextDangerIfErrors(controlName: string) {
        let styles = { 
            'text-danger': this.isInvalid(controlName),
        }
        return styles;
    }
    
    isInvalid(controlName: string): boolean {
        let control = this.signUpForm.get(controlName);
        if(control === null || control === undefined) return true;
        return (control.touched && (control.errors != null));
    }
}
