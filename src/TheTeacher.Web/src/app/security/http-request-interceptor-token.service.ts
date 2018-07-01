import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SecurityService } from './security.service';
import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';

@Injectable()
export class HttpRequestInterceptorTokenService implements HttpInterceptor {

    constructor(private cookieService: CookieService) {

    }

    intercept(req: HttpRequest<any>, next: HttpHandler,): Observable<HttpEvent<any>> {
        const token = this.cookieService.get('auth');
        if (token) {
            const newReq = req.clone(
            {
                headers: req.headers.set('Authorization', 'Bearer ' + token),
            });

            return next.handle(newReq);
        } else {
            return next.handle(req);
        }
    }
}
