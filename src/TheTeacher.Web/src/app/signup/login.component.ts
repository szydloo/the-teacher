import { Component, OnInit, } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { LoginService } from './login.service';
import { LoginUser } from '../models/commands/loginUser';
import { MatCheckbox } from '@angular/material';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
    title: string = "Log In!";
    logInForm: FormGroup;

    constructor(private fb: FormBuilder,private loginService: LoginService) { }

    ngOnInit() {
        this.logInForm = this.fb.group({
            email: ['',[Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+')]],
            password: ['', [Validators.required]]
        });
    }


    logIn() {
        const loginCommand: LoginUser = Object.assign({}, this.logInForm.value);
        console.log(JSON.stringify(loginCommand));
        this.loginService.loginUser(loginCommand).subscribe((data) => console.log(data),
                                                            (err) => console.log(err));
                                                
    }

}
