import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeacherService } from './teacher.service';
import { TeacherListComponent } from './teacher-list.component';

@NgModule({
    declarations: [
        TeacherListComponent
    ],
    imports: [
        CommonModule,
    ],
    
})
export class TeacherModule { }
