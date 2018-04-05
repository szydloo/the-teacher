import { Component, OnInit, } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatCheckbox } from '@angular/material';
import * as JWT from 'jwt-decode';

import { LoginService } from './login.service';
import { LoginUserCommand } from '../models/commands/login-user-command';
import { SecurityService } from '../security/security.service';
import { UserAuth } from '../models/security/user-auth';
import { User } from '../models/user';
import { Jwt } from '../models/security/jwt';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
    title: string = "Log In!";
    logInForm: FormGroup;
    returnUrl: string;

    constructor(private fb: FormBuilder,private loginService: LoginService, private securityService: SecurityService,
                private router: Router, private route: ActivatedRoute) {

                }

    ngOnInit() {
        this.logInForm = this.fb.group({
            email: ['testUsername@tet.com',[Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+')]],
            password: ['testSecret', [Validators.required]]
        });

        this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
    }

    logIn() {
        this.securityService.resetSecurityObject();

        // Get data from form
        const loginCommand: LoginUserCommand = Object.assign({}, this.logInForm.value);

        // Send login user command to api
        this.loginService.loginUser(loginCommand).subscribe((data) => this.setCurrentSecObject(data),
                                                            (err) => console.log(err))
                                                            
    }

    redirectToPage() {
        
        this.router.navigateByUrl(this.returnUrl);
    }

    setCurrentSecObject(jwt: Jwt) {
        let userAuth: UserAuth = new UserAuth();
        if(jwt.token.length > 0 && jwt != null) {
            console.log(JWT(jwt.token));
            userAuth.isAuthenticated = true;
            userAuth.token = jwt.token;
            // userAuth.role = JWT(jwt.token).role
            Object.assign(this.securityService.securityObject, userAuth);
            localStorage.setItem("bearerToken", this.securityService.securityObject.token);

            this.redirectToPage();
        }
    }

}
