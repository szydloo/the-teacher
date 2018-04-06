import { Component } from '@angular/core';
import { SecurityService } from './security/security.service';
import { UserAuth } from './models/security/user-auth';
import { LoginService } from './signup/login.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent {
    title = 'Welcome to the Teacher App';
    securityObject: UserAuth;

    constructor(private securityService: SecurityService) {
        this.securityObject = securityService.securityObject;
    }

    logout() {
        this.securityService.resetSecurityObject();
        this.securityObject = null;
    }
}
