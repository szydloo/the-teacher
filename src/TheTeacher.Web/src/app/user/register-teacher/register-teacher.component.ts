import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TeacherService } from '../../teacher/teacher.service';

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
            fullName: ["", Validators.required],
            addressGroup: this.fb.group({
                street: ["", Validators.required],
                zipcode: ["", Validators.required],
                city: ["", Validators.required],
                country: ["", Validators.required]
            })
        })
    };

    saveTeacher() {
        this.teacherService
    }

}
