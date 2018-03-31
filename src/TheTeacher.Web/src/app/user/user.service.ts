import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
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

    

    handleError(err: HttpErrorResponse) {
        // TODO: Log into error database
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
