import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { TeacherListComponent } from './teacher/teacher-list.component';
import { AuthGuard } from './security/auth.guard';
import { UserProfileComponent } from './user/user-profile.component';
import { AlertFilledFormsGuard } from './signup/alert-filled-forms.guard';
import { PersonalInfoEditComponent } from './user/personal-info-edit/personal-info-edit.component';

const routes: Routes = [
    { path: 'signup', component: SignupComponent, canDeactivate: [AlertFilledFormsGuard]},
    { path: 'teachers', component: TeacherListComponent, canActivate: [AuthGuard] }, 
    { 
        path: 'profile',
        component: UserProfileComponent,
        canActivate: [AuthGuard]
    }, 
    { path: 'profile/edit', component: PersonalInfoEditComponent, canActivate: [AuthGuard] },
    { path: 'home', component: HomeComponent},
    { path: '**',  pathMatch: 'full', redirectTo: 'home'},

];

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
