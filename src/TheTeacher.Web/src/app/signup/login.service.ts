import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

import { LoginUserCommand } from '../models/commands/login-user-command';
import { Jwt } from '../models/security/jwt';

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
                        .catch(this.handleError);
    }

    private handleError(err: HttpResponse<any>): ErrorObservable {
        return Observable.throw(err);
    }

}
