import { Component, OnInit } from '@angular/core';
import { SecurityService } from './security/security.service';
import { UserAuth } from './models/security/user-auth';
import { LoginService } from './signup/login.service';
import { CookieService } from 'ngx-cookie-service';
import * as JWT from 'jwt-decode';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
    title = 'Welcome to the Teacher App';
    securityObject: UserAuth;

    constructor(private securityService: SecurityService, private cookieService: CookieService) {
        this.securityObject = securityService.securityObject;
    }

    ngOnInit(): void {
        const token = this.cookieService.get('auth');   // TODO refactor into login service? other class? auth keyword access from settings     
        if(token.length > 0 && token != null) {

            const userAuth = new UserAuth();
            const decodedToken: any = JWT(token);

            userAuth.isAuthenticated = true;
            userAuth.token = token;
            userAuth.username = decodedToken.username;
            userAuth.userId = decodedToken.sub;
            userAuth.isTeacher = decodedToken.isTeacher.toLowerCase() == "true" ? true : false;

            // userAuth.role = JWT(jwt.token).role
            Object.assign(this.securityService.securityObject, userAuth);
        }
    }

    logout() {
        this.cookieService.delete('auth'); 
        this.securityService.resetSecurityObject();
        this.securityObject = null;
    }
}
