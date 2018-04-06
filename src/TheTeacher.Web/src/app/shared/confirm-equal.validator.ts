import { AbstractControl } from '@angular/forms';

export function confirmEqualPasswordValidator(passwordGroup: AbstractControl): { [key: string]: any; }| null {
    const password = passwordGroup.get('password');
    const confirmPassword = passwordGroup.get('confirmPassword');

    if (password && confirmPassword && (password.value !== confirmPassword.value)) {
        return { 'notEqual': true };
    } else {
        return null;
    }
}

export function confirmEqualEmailValidator(emailGroup: AbstractControl): { [key: string]: any; }| null {
    const email = emailGroup.get('email');
    const confirmEmail = emailGroup.get('confirmEmail');

    if (email && confirmEmail && (email.value !== confirmEmail.value)) {
        return { 'notEqual': true };
    } else {
        return null;
    }
}
