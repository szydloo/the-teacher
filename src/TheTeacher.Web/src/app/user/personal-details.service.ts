import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { UpdatePersonalDetailsInfo } from './commands/update-personal-details-info.command';



@Injectable()
export class PersonalDetailsService {
    url = 'http://localhost:5000/PersonalDetails/';
    options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};
    
    constructor(private client: HttpClient) {
    }

    updatePersonalDetailsInfo(command: UpdatePersonalDetailsInfo): Observable<HttpResponse<UpdatePersonalDetailsInfo>> {
        let body = JSON.stringify(command);
        let infoUrl = "";
        infoUrl = this.url + "info";
        return this.client.put(infoUrl, body, this.options)
                    .catch((err) => this.handleError(err));
    }

    handleError(err: HttpResponse<any>) {
        
        return Observable.throw(err);
    }
}

