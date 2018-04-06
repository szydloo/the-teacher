import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

export class HttpRequestInterceptorToken implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = localStorage.getItem('bearerToken');
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
