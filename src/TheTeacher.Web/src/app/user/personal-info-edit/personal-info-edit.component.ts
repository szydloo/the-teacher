import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { PersonalDetailsService } from '../personal-details.service';
import { UpdatePersonalDetailsInfo } from '../commands/update-personal-details-info.command';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonalDetails } from '../../models/personal-details';
import { UpdateImage } from '../commands/update-image.command';

@Component({
    selector: 'app-personal-info-edit',
    templateUrl: './personal-info-edit.component.html',
    styleUrls: ['./personal-info-edit.component.css']
})
export class PersonalInfoEditComponent implements OnInit {
    editError: boolean;
    editSuccess: boolean;
    editErrorMessage: string;
    editSuccessMessage: string;
    title: string = "Edit your personal info";
    editPersonalInfo: FormGroup;
    editImage: FormGroup;
    datePickerConfig: Partial<BsDatepickerConfig>;
    personalDetails: PersonalDetails = new PersonalDetails();
    image: Uint8Array;


    constructor(private fb: FormBuilder, private personalDetailsService: PersonalDetailsService, 
        private route: ActivatedRoute, private router: Router) {

        this.datePickerConfig = Object.assign({}, 
            { 
                containerClass: 'theme-dark-blue',
                showWeekNumbers: false,
                dateInputFormat: 'YYYY-MM-DD'
            });
     }

    ngOnInit() {
        this.personalDetails = this.route.snapshot.data['personalDetails'];
        this.image = this.route.snapshot.data['image'];
        
        // Change date format for sync with datepicker
        let myDate = this.personalDetails.dateOfBirth.toString().substr(0,10);

        this.editPersonalInfo = this.fb.group({
            firstName: [this.personalDetails.firstName ],
            lastName: [this.personalDetails.lastName],
            dateOfBirth: [myDate ],
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

        this.editImage = this.fb.group({
            image: [null],
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
        setTimeout(() => {
            this.router.navigate(['profile']), 5000;
        })
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
