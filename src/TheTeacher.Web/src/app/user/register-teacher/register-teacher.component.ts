import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TeacherService } from '../../teacher/teacher.service';
import { RegisterTeacherCommand } from '../../models/commands/teacher/register-teacher';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-register-teacher',
    templateUrl: './register-teacher.component.html',
    styleUrls: ['./register-teacher.component.css']
})
export class RegisterTeacherComponent implements OnInit {
    private teacherForm: FormGroup;

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
        
        this.teacherService.saveTeacher(registerTeacher).subscribe(() => this.onSaveComplete,
                                                                    (err) => this.handleError(err));
    }

    handleError(err: any) {
        console.error(err);
    }

    onSaveComplete() {
        this.teacherForm.reset();
    }

}
