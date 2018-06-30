import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
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
                    .catch(this.handleError);
    }
    
    getTearchersGridModel(): Observable<TeacherGridModelItem[]> {
        let urlGrid = this.url + 'GetTeachersGridModel';

        return this._client.get<TeacherGridModelItem[]>(urlGrid)
                            .catch(this.handleError);
    }

    saveTeacher(registerTeacher: RegisterTeacherCommand): Observable<HttpResponse<any>> {
        const body = JSON.stringify(registerTeacher);

        return this._client.post(this.url, body, this.options)
                            .catch(this.handleError);
    }

    handleError(err: HttpResponse<any>) {
        
        return Observable.throw(err);
    }
}
