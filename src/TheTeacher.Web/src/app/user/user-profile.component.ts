import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import 'rxjs/add/operator/debounceTime';

import { SecurityService } from '../security/security.service';
import { confirmEqualPasswordValidator } from '../shared/confirm-equal.validator';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
    timeToCheck: boolean;
    editPasswordForm: FormGroup;
    title: string = "";
    constructor(private fb: FormBuilder, private securityService: SecurityService) { }

    ngOnInit() {
        this.editPasswordForm = this.fb.group({
            currPassword: [''],
            passwordGroup: this.fb.group({
                password: [''],
                confirmPassword: ['']
            }, {validator: confirmEqualPasswordValidator}),
        })

        this.title = this.securityService.securityObject.username + "'s Profile" ;

        const confPasswordControl = this.editPasswordForm.get('passwordGroup.confirmPassword').valueChanges.debounceTime(1000).subscribe(() => this.timeToCheck = true);
    }

    

}
