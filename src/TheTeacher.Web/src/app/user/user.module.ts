import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { MatCheckboxModule } from '@angular/material';

import { UserService } from './user.service';
import { UserProfileComponent } from './user-profile.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { RegisterTeacherComponent } from './register-teacher/register-teacher.component';
import { TeacherModule } from '../teacher/teacher.module';
import { AddLessonComponent } from './add-lesson/add-lesson.component';
import { SubjectProviderService } from './add-lesson/subject-provider.service';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BrowserModule,
    MatCheckboxModule,
  ],
  declarations: [UserProfileComponent, ChangePasswordComponent, RegisterTeacherComponent, AddLessonComponent],
  providers: [UserService, SubjectProviderService]
})
export class UserModule { }
