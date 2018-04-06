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
    title = 'Log In!';
    logInForm: FormGroup;
    returnUrl = '';
    logingError = false;
    errorMessage = '';

    constructor(private fb: FormBuilder, private loginService: LoginService, private securityService: SecurityService,
                private router: Router, private route: ActivatedRoute) {

    }

    ngOnInit() {
        this.logInForm = this.fb.group({
            email: ['testUsername@tet.com', [Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+')]],
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
                                                            (err) => this.handleLoginError(err));

    }

    redirectToPage() {
        if (this.returnUrl) {
        this.router.navigateByUrl(this.returnUrl);
        } else {
            this.router.navigateByUrl('home');
        }
    }

    setCurrentSecObject(jwt: Jwt) {
        const userAuth: UserAuth = new UserAuth();
        if (jwt.token.length > 0 && jwt != null) {
            const decodedToken: any = JWT(jwt.token);
            console.log(decodedToken);

            userAuth.isAuthenticated = true;
            userAuth.token = jwt.token;
            userAuth.username = decodedToken.username;
            userAuth.userId = decodedToken.sub;

            // userAuth.role = JWT(jwt.token).role
            Object.assign(this.securityService.securityObject, userAuth);
            localStorage.setItem('bearerToken', this.securityService.securityObject.token);

            this.redirectToPage();
        }
    }

    handleLoginError(err: any) {
        console.log(err);
        // Synchronised with api
        if (err.error != null && err.error.code === 'invalid_credentials' ) {
            console.log(err.error.message);
            this.logingError = true;
            this.errorMessage = err.error.message;
        }

    }

}
