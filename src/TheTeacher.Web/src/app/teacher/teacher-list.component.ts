import { Component, OnInit, AfterContentInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataSource, CollectionViewer } from '@angular/cdk/collections';
import { Observable, of } from 'rxjs';

import { TeacherService } from './teacher.service';
import { Teacher } from '../models/teacher';
import { UserService } from '../user/user.service';
import { User } from '../models/user';
import { PersonalDetails } from '../models/personal-details';
import { Lesson } from '../models/lesson';
import { TeacherGridModelItem } from '../models/teacher-grid-model-item';

@Component({
    selector: 'app-teacher-list',
    templateUrl: './teacher-list.component.html',
    styleUrls: ['./teacher-list.component.css']
})
export class TeacherListComponent implements OnInit {
    title = `Teacher's list`;
    isTableToggled: boolean[];
    displayedColums:  string[] = ['email'];
    dataSource: TeacherDataSource;

    constructor(private _router: Router,
                private _teacherService: TeacherService,
                private _userSevice: UserService) { }

    ngOnInit() {
        this.dataSource = new TeacherDataSource(this._teacherService);
    }
}

export class TeacherDataSource extends DataSource<any> {
    constructor(private _teacherService: TeacherService) {
        super();
    }

    connect(): Observable<any> {

        return of({email: 'awdawd@awd.com'});
        // this._teacherService.getTearchersGridModel();
    }
    disconnect(collectionViewer: CollectionViewer): void {

    }
} 

