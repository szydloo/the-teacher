import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { TeacherModule } from './teacher/teacher.module';
import { AppRoutingModule } from './app-routing.module';
import { UserModule } from './user/user.module';
import { LoginComponent } from './signup/login.component';
import { SignupLoginModule } from './signup/signup-login.module';
import { ConfirmEqualValidatorDirective } from './shared/confirm-equal-validator.directive';
import { SecurityService } from './security/security.service';
import { HttpRequestInterceptorTokenModule } from './security/http-interceptor-token.module';
import { AuthGuard } from './security/auth.guard';
import { TeacherService } from './teacher/teacher.service';
import { TeacherListComponent } from './teacher/teacher-list.component';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        UserModule,
        TeacherModule,
        HttpClientModule,
        SignupLoginModule,
        SignupLoginModule,
        BrowserAnimationsModule,
        HttpRequestInterceptorTokenModule,
    ],
    providers: [SecurityService, AuthGuard, TeacherService],
    bootstrap: [AppComponent]
})
export class AppModule { }
