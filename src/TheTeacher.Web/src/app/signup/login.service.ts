
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';

import { Jwt } from '../models/security/jwt';
import { LoginUserCommand } from '../models/commands/user/login-user-command';
import { catchError } from 'rxjs/operators';

@Injectable()
export class LoginService {
    url = 'http://localhost:5000/login';

    constructor(private client: HttpClient) { }

    loginUser(loginUser: LoginUserCommand): Observable<Jwt> {
        const body = JSON.stringify(loginUser);
        const options = {
            headers: new HttpHeaders({'Content-Type': 'application/json'})
        };

        return this.client.post<Jwt>(this.url, body, options)
                        .pipe(catchError(this.handleError));

    }

    private handleError(err: HttpResponse<any>) {
        return observableThrowError(err);
    }

}
