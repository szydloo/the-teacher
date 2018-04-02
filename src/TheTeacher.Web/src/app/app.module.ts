import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { TeacherComponent } from './teacher/teacher.component';
import { TeacherModule } from './teacher/teacher.module';
import { AppRoutingModule } from './app-routing.module';
import { UserModule } from './user/user.module';
import { LoginComponent } from './signup/login.component';
import { SignupLoginModule } from './signup/signup-login.module';
import { ConfirmEqualValidatorDirective } from './shared/confirm-equal-validator.directive';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
    ],
    imports: [
        BrowserModule,
        TeacherModule,
        AppRoutingModule,
        UserModule,
        HttpClientModule,
        SignupLoginModule,
        SignupLoginModule,
        BrowserAnimationsModule,
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
