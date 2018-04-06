import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SecurityService } from './security.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private sercurityService: SecurityService, private router: Router) {
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        if (this.sercurityService.securityObject.isAuthenticated) {
            return true;
        } else {
            this.router.navigate(['signup'], {queryParams: { returnUrl: state.url }});
        }
    }
}
