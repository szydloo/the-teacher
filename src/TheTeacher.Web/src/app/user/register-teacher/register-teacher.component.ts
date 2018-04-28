import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TeacherService } from '../../teacher/teacher.service';
import { RegisterTeacherCommand } from '../../models/commands/teacher/register-teacher';
import { HttpErrorResponse } from '@angular/common/http';
import { ServiceErrorCodes } from '../../exception/service-error-codes';

@Component({
    selector: 'app-register-teacher',
    templateUrl: './register-teacher.component.html',
    styleUrls: ['./register-teacher.component.css']
})
export class RegisterTeacherComponent implements OnInit {
    private teacherForm: FormGroup;
    registerError: boolean = false;
    registerErrorMessage: string;
    registerSuccess: boolean = false;
    registerSuccessMessage: string;

    constructor(private fb: FormBuilder, private teacherService: TeacherService) { }

    ngOnInit() {
        this.teacherForm = this.fb.group({
            fullName: ["Jack Test", Validators.required],
            addressGroup: this.fb.group({
                street: ["testingMyStreet", Validators.required],
                zipcode: ["420-42", Validators.required],
                city: ["Warsaw", Validators.required],
                country: ["Poland", Validators.required]
            })
        })
    };

    saveTeacher() {
        // Make it pretier
        let registerTeacher: RegisterTeacherCommand = new RegisterTeacherCommand();
        registerTeacher.fullname = this.teacherForm.controls.fullName.value;
        registerTeacher.city = this.teacherForm.controls.addressGroup.value.city;
        registerTeacher.street = this.teacherForm.controls.addressGroup.value.street;
        registerTeacher.zipcode = this.teacherForm.controls.addressGroup.value.zipcode;
        registerTeacher.country = this.teacherForm.controls.addressGroup.value.country;
        
        this.teacherService.saveTeacher(registerTeacher).subscribe(() => this.onSaveComplete(),
                                                                    (err) => this.handleError(err));
    }

    handleError(err: any) {
        this.registerError = true;
        this.registerSuccess = false;

        switch(err.error.code) {
            case ServiceErrorCodes.teacherAlreadyExists:
                this.registerErrorMessage = "Teacher already exists.";
            break;
            default:
                this.registerErrorMessage = "Unknown error please try again later."
            break;
        }
        console.error(err);
    }

    onSaveComplete() {
        this.registerSuccess = true;
        this.registerError = false;

        this.registerSuccessMessage = "User succesfully added as a Teacher."
        this.teacherForm.reset();
    }

}
