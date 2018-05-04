import { Component, OnInit } from '@angular/core';
import { PersonalDetails } from '../../models/personal-details';
import { UserService } from '../user.service';
import { SecurityService } from '../../security/security.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-personal-info',
    templateUrl: './personal-info.component.html',
    styleUrls: ['./personal-info.component.css']
})
export class PersonalInfoComponent implements OnInit {
    usersPersonalDetails: PersonalDetails = new PersonalDetails();

    constructor(private userService: UserService, private securityService: SecurityService, private router: Router) { }

    ngOnInit() {
        let userId = this.securityService.securityObject.userId;
        this.userService.getUser(userId).subscribe((data) => {
            this.usersPersonalDetails = data.personalDetails;
        });
    }

    editPersonalInfo() {
        this.router.navigateByUrl('profile/edit')
    }
}
