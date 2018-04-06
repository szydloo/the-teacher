import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'app-signup-login-result-dialog',
    templateUrl: './signup-login-result-dialog.component.html',
    styles: [],
})
export class SignupLoginResultDialogComponent {

    constructor(public dialogRef: MatDialogRef<SignupLoginResultDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

    onOkClick() {
        this.dialogRef.close();
    }
}
