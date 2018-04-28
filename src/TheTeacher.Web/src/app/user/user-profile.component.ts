import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../security/security.service';
import { UserAuth } from '../models/security/user-auth';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
    title: string = '';
    securityObject: UserAuth;

    constructor(private securityService: SecurityService) {
    }

    ngOnInit(): void {
        this.securityObject = this.securityService.securityObject;
        this.title = this.securityObject.username + "'s profile";
    }


}
