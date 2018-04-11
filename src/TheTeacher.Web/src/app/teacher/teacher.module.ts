import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeacherListComponent } from './teacher-list.component';
import { TeacherComponent } from './teacher.component';
import { TeacherService } from './teacher.service';
import { AddLessonComponent } from './add-lesson.component';

@NgModule({
    declarations: [
        TeacherComponent,
        TeacherListComponent,
        AddLessonComponent
    ],
    imports: [
        CommonModule,
    ],
    providers: [TeacherService],
})
export class TeacherModule { }
