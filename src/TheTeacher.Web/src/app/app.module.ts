import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { TeacherComponent } from './teacher/teacher.component';
import { TeacherModule } from './teacher/teacher.module';
import { AppRoutingModule } from './app-routing.module';
import { UserService } from './shared/user.service';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        SignupComponent,
    ],
    imports: [
        BrowserModule,
        TeacherModule,
        AppRoutingModule
    ],
    providers: [UserService],
    bootstrap: [AppComponent]
})
export class AppModule { }
