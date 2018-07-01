
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';

import { RegisterUserCommand } from '../models/commands/user/register-user';
import { ChangeUserPasswordCommand } from '../models/commands/user/change-user-password-command';
import { User } from '../models/user';
import { catchError } from 'rxjs/operators';


@Injectable()
export class UserService {
    url = 'http://localhost:5000/users/';
    options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

    constructor(private client: HttpClient) {
    }

    getUsers(): Observable<User[]> {
        return this.client.get<User[]>(this.url)
                    .pipe(catchError(this.handleError));
    }

    getUser(userId: string): Observable<User> {
        const getUserUrl = this.url + userId;
        return this.client.get<User>(getUserUrl)
                    .pipe(catchError(this.handleError));
        
    }

    getUsersForIdsList(guids: string[]): Observable<User[]> {
        const getUsersUrl = this.url + 'UsersForIds';
        const body = guids;

        return this.client.post<User[]>(getUsersUrl, body, this.options)
                                .pipe(catchError(this.handleError));
        
    }

    saveUser(registerUser: RegisterUserCommand): Observable<any> {
        const body = JSON.stringify(registerUser);

        return this.client.post(this.url, body, this.options)
                        .pipe(catchError(this.handleError));
        
    }

    changePassword(changePassword: ChangeUserPasswordCommand): Observable<any> {
        const body = JSON.stringify(changePassword);

        return this.client.put(this.url + 'password', body, this.options)
                        .pipe(catchError(this.handleError));
        
    }

    handleError(err: HttpResponse<any>) {
        return observableThrowError(err);
    }
}
