import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../security/security.service';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
    title:string = "";

    constructor(private securityService: SecurityService) {

    }

    ngOnInit(): void {
        this.title = this.securityService.securityObject.username + "'s Profile" ;
        
    }

    
}
