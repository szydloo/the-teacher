import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

import { User } from '../models/user';


@Injectable()
export class UserService {
    url: string = 'http://localhost:5000/users';

    constructor(private _client: HttpClient) { 
    }

    getUsers(): Observable<User[]> {
        return this._client.get<User[]>(this.url)
                    .catch(this.handleError);
    }

    saveUser(user: User): Observable<HttpResponse<User>> {
        const options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};
        console.log('save user service: ' + JSON.stringify(user));
        const body = JSON.stringify(user);
        return this._client.post(this.url, body, options)
                            .catch(this.handleError);
    }

    handleError(err: HttpErrorResponse) {
        // TODO: Log into error database // Handle error sent by api
        let errMessage = '';
        if(err.error instanceof Error) {
            errMessage = `An error occured: ${err.error.message}`; 
        } else {
            errMessage = `Server returned code: ${err.status}, error message: ${err.message}`;
        }

        console.error(errMessage);
        return Observable.throw(errMessage);
    }
}
