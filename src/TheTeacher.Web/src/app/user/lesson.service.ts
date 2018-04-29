import { Injectable } from '@angular/core';
import { SecurityService } from '../security/security.service';
import { HttpClient, HttpResponse, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { AddLessonCommand } from './add-lesson/addLessonCommand';
import { Observable } from 'rxjs/Observable';
import { Lesson } from '../models/lesson';

@Injectable()
export class LessonService {
    url: string = 'http://localhost:5000/lessons';
    options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

    constructor( private client: HttpClient, private securityService: SecurityService) { }

    addLesson(addLesson: AddLessonCommand): Observable<HttpResponse<any>> {
        const body = JSON.stringify(addLesson);
        return this.client.post(this.url, body, this.options)
                            .catch(this.handleError);
    }
    
    getLessons(): Observable<Lesson[]> {
        return this.client.get(this.url + "/all/" + this.securityService.securityObject.userId, this.options)
                    .catch(this.handleError);
    }

    private handleError(err: HttpResponse<any>) {
        return Observable.throw(err);
    }
}
