import { Component, OnInit } from '@angular/core';
import { PersonalDetails } from '../../models/personal-details';
import { UserService } from '../user.service';
import { SecurityService } from '../../security/security.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-personal-info',
    templateUrl: './personal-info.component.html',
    styleUrls: ['./personal-info.component.css']
})
export class PersonalInfoComponent implements OnInit {
    usersPersonalDetails: PersonalDetails;

    constructor(private userService: UserService, 
            private securityService: SecurityService, 
            private router: Router,
            private route: ActivatedRoute) { }

    ngOnInit() {
        let userId = this.securityService.securityObject.userId;

        this.usersPersonalDetails = this.route.snapshot.data['personalDetails'];
    }

    editPersonalInfo() {
        this.router.navigate(['edit']);
    }
}
