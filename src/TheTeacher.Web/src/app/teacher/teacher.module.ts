import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeacherListComponent } from './teacher-list.component';
import { TeacherComponent } from './teacher.component';
import { TeacherService } from './teacher.service';

@NgModule({
    declarations: [
        TeacherComponent,
        TeacherListComponent
    ],
    imports: [
        CommonModule,
    ],
    providers: [TeacherService],
})
export class TeacherModule { }
