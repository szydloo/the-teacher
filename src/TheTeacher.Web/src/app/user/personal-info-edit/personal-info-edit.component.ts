import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { PersonalDetailsService } from '../personal-details.service';
import { UpdatePersonalDetailsInfo } from '../commands/update-personal-details-info.command';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonalDetails } from '../../models/personal-details';

@Component({
    selector: 'app-personal-info-edit',
    templateUrl: './personal-info-edit.component.html',
    styleUrls: ['./personal-info-edit.component.css']
})
export class PersonalInfoEditComponent implements OnInit {
    title: string = "Edit your personal info";
    editPersonalInfo: FormGroup;
    datePickerConfig: Partial<BsDatepickerConfig>;
    personalDetails: PersonalDetails = new PersonalDetails();


    constructor(private fb: FormBuilder, private personalDetailsService: PersonalDetailsService, 
        private route: ActivatedRoute, private router: Router) {

        this.datePickerConfig = Object.assign({}, 
            { 
                containerClass: 'theme-dark-blue',
                showWeekNumbers: false,
                dateInputFormat: 'DD/MM/YYYY'
            });
     }

    ngOnInit() {
        this.personalDetails = this.route.snapshot.data['personalDetails'];

        this.editPersonalInfo = this.fb.group({
            firstName: [this.personalDetails.firstName ],
            lastName: [this.personalDetails.lastName],
            dateOfBirth: [this.personalDetails.dateOfBirth ],
            address: this.fb.group({
                street: [this.personalDetails.address.street],
                zipcode: [this.personalDetails.address.zipcode],
                city: [this.personalDetails.address.city],
                country: [this.personalDetails.address.country]
            }),
            university: [this.personalDetails.university],
            fieldOfStudy: [this.personalDetails.fieldOfStudy],
            title: [this.personalDetails.title],

        })
    }

    submitPersonalInfo() {
        let command: UpdatePersonalDetailsInfo = new UpdatePersonalDetailsInfo();

        // TODO: UGH Gotta be better idea
        command.firstName = this.editPersonalInfo.controls.firstName.value;
        command.lastName = this.editPersonalInfo.controls.lastName.value;
        command.dateOfBirth = (<Date>this.editPersonalInfo.controls.dateOfBirth.value);
        command.address.city = this.editPersonalInfo.controls.address.value.city;
        command.address.street = this.editPersonalInfo.controls.address.value.street;
        command.address.zipcode = this.editPersonalInfo.controls.address.value.zipcode;
        command.address.country = this.editPersonalInfo.controls.address.value.country;
        command.university = this.editPersonalInfo.controls.university.value;
        command.fieldOfStudy = this.editPersonalInfo.controls.fieldOfStudy.value;
        command.title = this.editPersonalInfo.controls.title.value;
        
        this.personalDetailsService.updatePersonalDetailsInfo(command).subscribe((data) => this.onSuccessUpdate(data),
                                                                                (err) => this.handleError(err));
    }

    onSuccessUpdate(data: any) {
        console.log(data);
        // TODO: router navigate to profile
        this.router.navigate(['profile']);
    }

    handleError(err: any) {
        console.error(err);
        // TODO: Error msg
    }

    backToProfile() {
        if(this.editPersonalInfo.touched ) {
            if(window.confirm('Are you sure? Your changes will be discarded.')) {
                this.router.navigate(['profile']);
            }
        } else {
            this.router.navigate(['profile']);
        }
    }
}
