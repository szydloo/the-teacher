import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeacherListComponent } from './teacher-list.component';
import { TeacherComponent } from './teacher.component';
import { TeacherService } from './teacher.service';
import { TeacherListRoutingModule } from './teacher-list-routing.module';

@NgModule({
    declarations: [
        TeacherComponent,
        TeacherListComponent
    ],
    imports: [
        CommonModule,
        TeacherListRoutingModule,
    ],
    providers: [TeacherService],
})
export class TeacherModule { }
