import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { SubjectProviderService } from './subject-provider.service';
import { Subject } from '../../models/subject';

@Component({
    selector: 'app-add-lesson',
    templateUrl: './add-lesson.component.html',
    styleUrls: ['./add-lesson.component.css']
})
export class AddLessonComponent implements OnInit {
    title : string = "Add Lessons you're willing to teach.";
    addLessonForm: FormGroup;
    successMessage: string;
    addLessonSuccess: boolean = false;
    errorMessage: string;
    addLessonError: boolean = false;
    subjects: Subject[] = [];
    categories: string[] = [];
    names: string[] = [];
    
    constructor(private fb: FormBuilder, private subjectProvider: SubjectProviderService) { }

    ngOnInit() {
        this.addLessonForm = this.fb.group({
            subject: this.fb.group({
                category: this.fb.control("",Validators.required),
                name: this.fb.control({value: "", disabled:true}, Validators.required),
            }, {}),
            grade : ['', Validators.required],
            pricePerHour: [0, Validators.required]
        });

        this.addLessonForm.get('subject.category').valueChanges.subscribe((val) => {
            if(val === '0') {
                this.addLessonForm.get('subject.name').disable();
            } else {
                this.addLessonForm.get('subject.name').enable();
            }
        });

        this.initSubjects();
    }

    initSubjects() {
        this.subjectProvider.browseSubjects().subscribe((data) => this.subjects = data,
                                                        (err) => this.handleError(err),
                                                        () => { 
                                                            this.initCategories();
                                                        });
    }

    handleError(err: any) {
        console.error(JSON.stringify(err));
    }

    onSuccess() {

    }

    initCategories() {
        this.subjects.forEach((sub) => this.categories.push(sub.category));
        this.categories = this.categories.filter((x,i,a) => x && a.indexOf(x) === i);
    }

    setNames(category: string) {
        category = category.substring(3);
        this.names = this.subjects.map<string>((s) => {if(s.category === category) return s.name});
        this.names = this.names.filter((n) => { return n != undefined });
    }

    initNames() {

    }

}
