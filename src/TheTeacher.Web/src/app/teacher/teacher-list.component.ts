import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { TeacherService } from './teacher.service';
import { Teacher } from '../models/teacher';

@Component({
    selector: 'app-teacher-list',
    templateUrl: './teacher-list.component.html',
    styleUrls: ['./teacher-list.component.css']
})
export class TeacherListComponent implements OnInit {
    title = 'List of Teachers';
    teachers: Teacher[];
    isTableToggled: boolean[];

    constructor(private _router: Router,
                private _teacherService: TeacherService) { }

    ngOnInit() {
        this._teacherService.getTeachers().subscribe((data) => this.teachers = data,
                                                    (err) => console.log(err), // TODO proper err notificaton
                                                    () => this.setToggledTableVals() );
    }


    navigateToRegisterTeacherForm(): void {
    }

    // Table toggles
    setToggledTableVals() {
        this.isTableToggled = new Array<boolean>(this.teachers.length);
    }

    toggleProperTable(i): void {
        this.isTableToggled[i] = !this.isTableToggled[i];
    }
}
