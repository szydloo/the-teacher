import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

import { LoginUser } from '../models/commands/loginUser';

@Injectable()
export class LoginService {
    url: string = "http://localhost:5000/login"
    constructor(private client: HttpClient) { }

    loginUser(loginUser: LoginUser): Observable<HttpResponse<LoginUser>> {
        let body = JSON.stringify(loginUser);
        let options = {
            headers: new HttpHeaders({'Content-Type': 'application/json'})
        };

        return this.client.post(this.url, body, options)
                        .catch(this.handleError);
    }

    private handleError(err: HttpResponse<any>): ErrorObservable {
        return Observable.throw(err);
    }

}
