import { Component, OnInit } from '@angular/core';
import { Lesson } from '../../../models/lesson';
import { LessonService } from '../../lesson.service';

@Component({
    selector: 'app-lesson-list',
    templateUrl: './lesson-list.component.html',
    styleUrls: ['./lesson-list.component.css']
})
export class LessonListComponent implements OnInit {
    title: string = "Your current subjects"
    lessons: Lesson[] = [];

    constructor(private lessonService: LessonService) { }

    ngOnInit() {
        this.lessonService.getLessons().subscribe((data) => this.lessons = data, (err) => this.onFailure(err))
    }

    onFailure(err: any) {

    }

}
