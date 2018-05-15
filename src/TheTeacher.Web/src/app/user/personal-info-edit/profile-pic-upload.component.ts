import { Component, OnInit } from '@angular/core';
import { PersonalDetailsService } from '../personal-details.service';
declare const Buffer;

@Component({
  selector: 'app-profile-pic-upload',
  templateUrl: './profile-pic-upload.component.html',
  styleUrls: ['./profile-pic-upload.component.css']
})
export class ProfilePicUploadComponent implements OnInit {

    fileToUpload: File = null;
    image = null;

    constructor(private personalDetailsService: PersonalDetailsService) { }

    ngOnInit() {
    }

    onUpload(files: FileList) {
        this.fileToUpload = files.item(0);
    }

    saveImage() {

        this.personalDetailsService.updateImage(this.fileToUpload).subscribe(this.onSuccess,(err) => this.onError(err));
    }
    onSuccess() {
        console.log("hurray");

    }
    onError(err) {
        console.log(err);
    }
}
