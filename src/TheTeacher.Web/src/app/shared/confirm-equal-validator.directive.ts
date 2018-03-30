import { Directive, Input, OnInit } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';

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
export class ConfirmEqualValidatorDirective implements OnInit,Validator {


    @Input('appConfirmEqualValidator') params: string // = 'password' ;
    
    contrls: string[];
    confEqSecControlName: string;
    confEqFirstControlName: string;
    constructor() {
    }

    ngOnInit(): void {
        this.contrls = this.params.split(';')
        this.confEqFirstControlName = this.contrls[0];
        this.confEqSecControlName = this.contrls[1];
    }
    
    validate(controlGroup: AbstractControl): { [key: string]: any; } {
        console.log(this.confEqFirstControlName + ' ' + this.confEqSecControlName);
        
        let firstControl = controlGroup.get(this.confEqFirstControlName);
        let secControl = controlGroup.get(this.confEqSecControlName);
        
        console.log(firstControl.value + ' fc ' + secControl.value + ' sec');
        if (firstControl && secControl && ( firstControl.value !== secControl.value )) {
            return { 'notEqual': true};
        } else {
            return null;
        }
    }

    registerOnValidatorChange?(fn: () => void): void { 
        throw new Error("Method not implemented.");
    }
}
