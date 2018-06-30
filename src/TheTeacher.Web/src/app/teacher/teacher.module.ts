import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeacherService } from './teacher.service';
import { TeacherListComponent } from './teacher-list.component';
import { MatTableModule } from '@angular/material/table';

@NgModule({
    declarations: [
        TeacherListComponent
    ],
    imports: [
        CommonModule,
        MatTableModule
    ],
})
export class TeacherModule { }
