import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';

import { SecurityService } from '../../security/security.service';
import { UserService } from '../user.service';
import { confirmEqualPasswordValidator } from '../../shared/confirm-equal.validator';
import { ChangeUserPasswordCommand } from '../../models/commands/user/change-user-password-command';


@Component({
    selector: 'app-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
    timeToCheck: boolean;
    editPasswordForm: FormGroup;
    title: string = 'Change Password';
    changePasswordError: boolean = false;
    errorMessage: string = '';
    changePasswordSuccess: boolean = false;
    successMessage: string;

constructor(private fb: FormBuilder, private securityService: SecurityService, private userService: UserService) { }

    ngOnInit() {
        this.editPasswordForm = this.fb.group({
            currPassword: ['', Validators.required],
            passwordGroup: this.fb.group({
                password: [''],
                confirmPassword: ['']
            }, {validator: confirmEqualPasswordValidator}),
        });
        
        const confPasswordControl = this.editPasswordForm.get('passwordGroup.confirmPassword')
                                                            .valueChanges.pipe(debounceTime(1000)).subscribe(() => this.timeToCheck = true);
    }

    changePassword() {
        const changePassword: ChangeUserPasswordCommand = new ChangeUserPasswordCommand();
        changePassword.userId = this.securityService.securityObject.userId;
        changePassword.currentPassword = this.editPasswordForm.controls.currPassword.value;
        changePassword.newPassword = this.editPasswordForm.controls.passwordGroup.value.password;

        this.userService.changePassword(changePassword).subscribe((data) => this.changeSuccess(),
                                                                    (err) => this.handleError(err));
    }

    changeSuccess() {
        this.changePasswordError = false;
        this.changePasswordSuccess = true;  
        this.successMessage = "Password was changed succesfuly."
    }

    handleError(err: any) {
        if (err.error.message != null) {
            this.changePasswordSuccess = false;
            this.changePasswordError = true;
            this.errorMessage = err.error.message;
        }
    }
}
