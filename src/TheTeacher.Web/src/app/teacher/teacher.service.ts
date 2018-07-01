import { throwError as observableThrowError,  Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaderResponse, HttpHeaders, HttpResponse } from '@angular/common/http';

import { Teacher } from '../models/teacher';
import { RegisterTeacherCommand } from '../models/commands/teacher/register-teacher';
import { TeacherGridModelItem } from '../models/teacher-grid-model-item';


@Injectable()
export class TeacherService {
    url = 'http://localhost:5000/teachers/';
    options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

    constructor(private _client: HttpClient) { }

    getTeachers(): Observable<Teacher[]> {
        return this._client.get<Teacher[]>(this.url)
                        .pipe(catchError(this.handleError));
    }
    
    getTearchersGridModel(): Observable<TeacherGridModelItem[]> {
        let urlGrid = this.url + 'GetTeachersGridModel';

        return this._client.get<TeacherGridModelItem[]>(urlGrid)
                        .pipe(catchError(this.handleError));
    }

    saveTeacher(registerTeacher: RegisterTeacherCommand): Observable<any> {
        const body = JSON.stringify(registerTeacher);

        return this._client.post(this.url, body, this.options)
                            .pipe(catchError(this.handleError));
    }

    handleError(err: HttpResponse<any>) {
        
        return observableThrowError(err);
    }
}
