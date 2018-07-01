import {throwError as observableThrowError,  Observable} from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaderResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Subject } from '../../models/subject';

@Injectable()
export class SubjectProviderService {
    url = 'http://localhost:5000/subjects/';
    options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

    constructor(private _client: HttpClient) { }

    browseSubjects(): Observable<Subject[]> {
        return this._client.get<Subject[]>(this.url)
                            .pipe(catchError(this.handleError));
    }

    handleError(err: HttpResponse<any>) {
        
        return observableThrowError(err);
    }
}
