import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';

@Component({
    selector: 'app-personal-info-edit',
    templateUrl: './personal-info-edit.component.html',
    styleUrls: ['./personal-info-edit.component.css']
})
export class PersonalInfoEditComponent implements OnInit {
    title: string = "Edit your personal info";
    editPersonalInfo: FormGroup;
    datePickerConfig: Partial<BsDatepickerConfig>;


    constructor(private fb: FormBuilder) {
        this.datePickerConfig = Object.assign({}, 
            { 
                containerClass: 'theme-dark-blue',
                showWeekNumbers: false,
                dateInputFormat: 'DD/MM/YYYY'
            });
     }

    ngOnInit() {
        this.editPersonalInfo = this.fb.group({
            firstName: [null ],
            lastName: [null],
            dateOfBirth: ["01/01/1999"],
            address: this.fb.group({
                street: [null],
                zipcode: [null],
                city: [null],
                country: [null]
            }),
            university: [null],
            fieldOfStudy: [null],
            title: [null],

        })
    }

}
