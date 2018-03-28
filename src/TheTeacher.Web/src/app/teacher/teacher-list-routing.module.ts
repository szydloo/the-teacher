import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TeacherListComponent } from './teacher-list.component';
import { TeacherComponent } from './teacher.component';

const routes: Routes = [
    { path: 'teachers', 
        component: TeacherListComponent,
        children: [
            { path: 'register', component: TeacherComponent },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TeacherListRoutingModule { }
