import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { TeacherComponent } from './teacher/teacher.component';
import { TeacherModule } from './teacher/teacher.module';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        RegisterComponent,
    ],
    imports: [
        BrowserModule,
        TeacherModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
