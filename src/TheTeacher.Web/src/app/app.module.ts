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


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        SignupComponent,
    ],
    imports: [
        BrowserModule,
        TeacherModule,
        AppRoutingModule,
        UserModule,
        HttpClientModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
