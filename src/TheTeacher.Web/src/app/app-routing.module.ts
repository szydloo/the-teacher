import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { TeacherListComponent } from './teacher/teacher-list.component';

const routes: Routes = [
    { path: 'signup', component: SignupComponent},
    { path: 'teachers', component: TeacherListComponent},
    { path: 'home', component: HomeComponent},
    { path: '', component: HomeComponent},
    { path: '**',  pathMatch: 'full', redirectTo: 'home'}
]

@NgModule({
    declarations: [

    ],
    imports: [
        RouterModule.forRoot(routes),
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }