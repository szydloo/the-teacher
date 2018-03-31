import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    title: string = "Log In!";
    logInForm: FormGroup

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.logInForm = this.fb.group({
            email: ['',[Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+')]],
            password: ['', [Validators.required]]
        })
    }

}
