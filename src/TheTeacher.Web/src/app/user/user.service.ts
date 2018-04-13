import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

import { RegisterUserCommand } from '../models/commands/user/register-user';
import { ChangeUserPasswordCommand } from '../models/commands/user/change-user-password-command';
import { User } from '../models/user';


@Injectable()
export class UserService {
    url = 'http://localhost:5000/users/';
    options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

    constructor(private client: HttpClient) {
    }

    getUsers(): Observable<User[]> {
        return this.client.get<User[]>(this.url)
                    .catch(this.handleError);
    }

    saveUser(registerUser: RegisterUserCommand): Observable<HttpResponse<RegisterUserCommand>> {
        const body = JSON.stringify(registerUser);

        return this.client.post(this.url, body, this.options)
                            .catch(this.handleError);
    }

    changePassword(changePassword: ChangeUserPasswordCommand): Observable<HttpResponse<any>> {
        const body = JSON.stringify(changePassword);

        return this.client.put(this.url + 'password', body, this.options)
                            .catch(this.handleError);
    }

    handleError(err: HttpResponse<any>) {
        
        return Observable.throw(err);
    }
}
