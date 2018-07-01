import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { SignupComponent } from '../signup/signup.component';

@Injectable()
export class AlertFilledFormsGuard implements CanDeactivate<SignupComponent> {
    canDeactivate(component: SignupComponent, currentRoute: ActivatedRouteSnapshot,
             currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot): boolean {
        if (component.signUpForm.dirty) {
            return confirm('Are you sure you want to discard your changes?');
        }
        return true;
    }
}
