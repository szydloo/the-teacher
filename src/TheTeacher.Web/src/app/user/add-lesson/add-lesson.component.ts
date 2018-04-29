import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { SubjectProviderService } from './subject-provider.service';
import { Subject } from '../../models/subject';
import { AddLessonCommand } from './addLessonCommand';
import { LessonService } from '../lesson.service';

@Component({
    selector: 'app-add-lesson',
    templateUrl: './add-lesson.component.html',
    styleUrls: ['./add-lesson.component.css']
})
export class AddLessonComponent implements OnInit {
    title : string = "Add Subject you want to teach.";
    addLessonForm: FormGroup;
    successMessage: string;
    addLessonSuccess: boolean = false;
    errorMessage: string;
    addLessonError: boolean = false;
    subjects: Subject[] = [];
    categories: string[] = [];
    names: string[] = [];
    private grades: string[] = ["Starter", "Elementary", "Intermediate", "Upper Intermediate", "Expert", "Mastery"];
    
    constructor(private fb: FormBuilder,private lessonService: LessonService, private subjectProvider: SubjectProviderService) { }

    ngOnInit() {
        this.addLessonForm = this.fb.group({
            subject: this.fb.group({
                category: this.fb.control('0',[ Validators.required, Validators.min(2) ]),
                name: this.fb.control({value: '', disabled: true}, Validators.required),
            }, {}),
            grade : this.fb.control('0', [Validators.required, Validators.min(2)]),
            pricePerHour: [0, [Validators.required, Validators.min(0.01)]]
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
        console.error("here " + JSON.stringify(err));
    }

    addLesson() {
        let command: AddLessonCommand = new AddLessonCommand();
        command.category = this.addLessonForm.get('subject.category').value;
        command.name = this.addLessonForm.get('subject.name').value;
        command.grade = this.addLessonForm.get('grade').value;
        let pricePerHour = (<number>this.addLessonForm.get('pricePerHour').value).toFixed(2);
        command.pricePerHour = +pricePerHour;
        console.log(command.pricePerHour);
        this.lessonService.addLesson(command).subscribe(() => this.onAddSuccess(),
                                                        (err) => this.onAddFailure(err) )
    }
    onAddFailure(err: any) {
        this.addLessonSuccess = false;
        this.addLessonError = true;
        if(err.error.message != null) {
            this.errorMessage = err.error.message;
        } else {
            this.errorMessage = "Unexpected error please try again later.";
        }
    }

    onAddSuccess() {
        this.addLessonSuccess = true;
        this.addLessonError = false;
        this.successMessage = "Lesson added successfully."
    }

    initCategories() {
        this.subjects.forEach((sub) => this.categories.push(sub.category));
        this.categories = this.categories.filter((x,i,a) => x && a.indexOf(x) === i);
    }

    setNames(category: string) {
        // Removes, where i=index, 'i: ' which is passed from the $event.target.value TODO: Better solution
        category = category.substring(3);

        this.names = this.subjects.map<string>((s) => {if(s.category === category) return s.name});
        this.names = this.names.filter((n) => { return n != undefined });

        // Set default name for a category ie. Math - Calculus etc.
        this.addLessonForm.get('subject').patchValue({
            name: this.names[0],
        })
    }

}
