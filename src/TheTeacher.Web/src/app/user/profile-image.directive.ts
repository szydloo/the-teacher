import { Directive, OnInit, Input} from '@angular/core';
import { DomSanitizer} from '@angular/platform-browser';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

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
        .pipe(map(image => image.toString()))
        .subscribe(
            data => {
            this.imageData = 'data:image/jpeg;base64,' + data;
            this.sanitizedImageData = this.sanitizer.bypassSecurityTrustUrl(this.imageData);
            }, (err) => {
                console.warn(err);
            }
        );
    }
}