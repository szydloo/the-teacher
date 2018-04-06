import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { HttpRequestInterceptorToken } from './http-request-interceptor-token';

@NgModule({
    providers:  [
        {provide: HTTP_INTERCEPTORS,
        useClass: HttpRequestInterceptorToken,
        multi: true}
    ]})
export class HttpRequestInterceptorTokenModule { }
