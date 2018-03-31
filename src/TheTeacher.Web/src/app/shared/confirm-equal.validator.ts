import { AbstractControl } from "@angular/forms";

export function confirmEqualPasswordValidator(passwordGroup: AbstractControl): { [key: string]: any; }| null {
    let password = passwordGroup.get('password');
    let confirmPassword = passwordGroup.get('confirmPassword');

    if(password && confirmPassword && (password.value !== confirmPassword.value)) {
        return { 'notEqual': true };
    } else {
        return null;
    }
}

export function confirmEqualEmailValidator(emailGroup: AbstractControl): { [key: string]: any; }| null {
    let email = emailGroup.get('email');
    let confirmEmail = emailGroup.get('confirmEmail');

    if(email && confirmEmail && (email.value !== confirmEmail.value)) {
        return { 'notEqual': true };
    } else {
        return null;
    }
}