import { Injectable } from '@angular/core';
import { UserAuth } from '../models/security/user-auth';

@Injectable()
export class SecurityService {

    securityObject: UserAuth = new UserAuth();

    constructor() { }

    resetSecurityObject() {
        this.securityObject.token = "";
        this.securityObject.isAuthenticated = false;

        localStorage.removeItem("bearerToken");
    }

}
