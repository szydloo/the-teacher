import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-teacher-list',
    templateUrl: './teacher-list.component.html',
    styleUrls: ['./teacher-list.component.css']
})
export class TeacherListComponent implements OnInit {
    title: string;


    constructor(private _router: Router,) { }

    ngOnInit() {
    }

    navigateToRegisterTeacherForm(): void {
        // TODO
    }
}
