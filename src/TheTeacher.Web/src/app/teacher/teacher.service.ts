import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpErrorResponse, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';

import { Teacher } from '../models/teacher';
import { RegisterTeacherCommand } from '../models/commands/teacher/register-teacher';


@Injectable()
export class TeacherService {
    url = 'http://localhost:5000/teachers/';

    constructor(private _client: HttpClient) { }

    getTeachers(): Observable<Teacher[]> {
        return this._client.get<Teacher[]>(this.url)
                    .catch(this.handleError);
    }

    saveTeacher(registerTeacher: RegisterTeacherCommand): Observable<Teacher> {
        const body = JSON.stringify(registerTeacher);
        const options = {
            headers: new HttpHeaders({'Content-Type': 'application/json'})
        }
        return this._client.post(this.url, body, options)
                            .catch(this.handleError);
    }

    handleError(err: HttpErrorResponse) {
        // TODO: Log into error database
        let errMessage = '';
        if (err.error instanceof Error) {
            errMessage = `An error occured: ${err.error.message}`;
        } else {
            errMessage = `Server returned code: ${err.status}, error message: ${err.message}`;
        }

        console.error(errMessage);
        return Observable.throw(errMessage);
    }
}
