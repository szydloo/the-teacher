import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { HttpRequestInterceptorTokenService } from './http-request-interceptor-token.service';

@NgModule({
    providers:  [
        {provide: HTTP_INTERCEPTORS,
        useClass: HttpRequestInterceptorTokenService,
        multi: true}
    ]})
export class HttpRequestInterceptorTokenModule { }
