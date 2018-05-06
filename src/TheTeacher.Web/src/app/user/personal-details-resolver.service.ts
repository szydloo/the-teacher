import { Injectable } from '@angular/core';
import { PersonalDetails } from '../models/personal-details';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { UserService } from './user.service';
import { SecurityService } from '../security/security.service';
import 'rxjs/add/observable/of';

@Injectable()
export class PersonalDetailsResolver implements Resolve<PersonalDetails> {
    
    constructor(private userService: UserService, private securityService: SecurityService, private router: Router ) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):  Observable<PersonalDetails> {
        let userId = this.securityService.securityObject.userId;
        return this.userService.getUser(userId)
            .map((user) => {
                if(user.details) {
                    return user.details;
                }
                console.error(`Cannot obtain personal details of user with id ${userId} `);
                return null;
            })
            .catch((err) => {
                console.error(err);
                return Observable.of(null);
            })
        
    }

}
