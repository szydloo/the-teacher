import { Component, OnInit } from '@angular/core';
import { PersonalDetails } from '../../models/personal-details';
import { UserService } from '../user.service';
import { SecurityService } from '../../security/security.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PersonalDetailsService } from '../personal-details.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app-personal-info',
    templateUrl: './personal-info.component.html',
    styleUrls: ['./personal-info.component.css']
})
export class PersonalInfoComponent implements OnInit {

    usersPersonalDetails: PersonalDetails;
    userId: string;
    imageBase64: string;
    imagePath: any;

    constructor(private userService: UserService, 
            private securityService: SecurityService, 
            private router: Router,
            private route: ActivatedRoute,
            private personalDetailsService: PersonalDetailsService,
            private domSanitizer: DomSanitizer
            ) { }

    ngOnInit() {
        this.userId = this.securityService.securityObject.userId;
        this.usersPersonalDetails = this.route.snapshot.data['personalDetails'];
        this.personalDetailsService.getImage(this.userId)
                                    .subscribe(data => {this.imageBase64 = data.value; this.setImagePath()},
                                        (err) => this.handleError(err));

       
    }

    handleError(err) {
        console.warn(err);
    }

    setImagePath(): any {
        this.imagePath = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + this.imageBase64);
    }


    editPersonalInfo() {
        this.router.navigate(['edit']);
    }
}
