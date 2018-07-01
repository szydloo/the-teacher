
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';
import { UpdatePersonalDetailsInfo } from './commands/update-personal-details-info.command';
import { UpdateImage } from './commands/update-image.command';
import { catchError, map } from 'rxjs/operators';



@Injectable()
export class PersonalDetailsService {
    url = 'http://localhost:5000/PersonalDetails/';
    options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};
    
    constructor(private client: HttpClient) {
    }

    getImage(userId: string): Observable<any> {
        return this.client.get(this.url + "image/" + userId)
                        .pipe(catchError((err) => this.handleError(err)));
    }

    updatePersonalDetailsInfo(command: UpdatePersonalDetailsInfo): Observable<UpdatePersonalDetailsInfo> {
        let body = JSON.stringify(command);
        let infoUrl = "";
        infoUrl = this.url + "info";
        return this.client.put<UpdatePersonalDetailsInfo>(infoUrl, body, this.options)
                    .pipe(catchError((err) => this.handleError(err)));
    }

    updateImage(fileToUpload: File) : Observable<boolean> {
        const formData: FormData = new FormData();
        formData.append('image', fileToUpload, fileToUpload.name);

        let fileOptions = { headers: new HttpHeaders({ 'Content-Type': 'multipart/form-data;'})}

        let updateUrl = "";
        updateUrl = this.url + "image";
        
        return this.client.put<boolean>(updateUrl, formData,)
                        .pipe(map(() => { return true; }), catchError((err) => this.handleError(err)));
    }

    handleError(err: HttpResponse<any>) {
        
        return observableThrowError(err);
    }
}

