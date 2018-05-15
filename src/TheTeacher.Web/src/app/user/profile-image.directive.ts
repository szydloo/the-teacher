import {Directive, OnInit, Input} from '@angular/core';
import { DomSanitizer} from '@angular/platform-browser';
import { HttpClient } from '@angular/common/http';

@Directive({
  selector: '[profile-image]',
  host: {
    '[src]': 'sanitizedImageData'
  }
})
export class ProfileImageDirective implements OnInit {
    imageData: any;
    sanitizedImageData: any;
    @Input('profile-image') profileId: number;
    url = 'http://localhost:5000/PersonalDetails/image/';

    constructor(private httpClient: HttpClient,
                private sanitizer: DomSanitizer) { }

    ngOnInit() {        
        this.httpClient.get(this.url + this.profileId)
        .map(image => image.toString())
        .subscribe(
            data => {
            this.imageData = 'data:image/png;base64,' + data;
            }
        );
    }
}