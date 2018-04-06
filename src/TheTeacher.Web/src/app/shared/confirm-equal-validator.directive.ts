import { Directive, Input, OnInit } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';


// Deprecated/ Not Working
@Directive({
    selector: '[appConfirmEqualValidator]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useClass: ConfirmEqualValidatorDirective,
            multi: true,
        }
    ],

})
export class ConfirmEqualValidatorDirective implements OnInit, Validator {

    @Input() confEqSecControlName  = 'password' ;
    @Input() confEqFirstControlName  = 'confirmPassword' ;

    constructor() {
    }

    ngOnInit(): void {
    }

    validate(controlGroup: AbstractControl): { [key: string]: any; } {
        console.log(this.confEqFirstControlName + ' ' + this.confEqSecControlName);

        const firstControl = controlGroup.get(this.confEqFirstControlName);
        const secControl = controlGroup.get(this.confEqSecControlName);

        console.log(firstControl.value + ' fc ' + secControl.value + ' sec');
        if (firstControl && secControl && ( firstControl.value !== secControl.value )) {
            return { 'notEqual': true};
        } else {
            return null;
        }
    }

    registerOnValidatorChange?(fn: () => void): void {
        throw new Error('Method not implemented.');
    }
}
