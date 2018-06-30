import { Lesson } from './lesson';


export interface Teacher {
    id: string;
    userID: string;
    lessons: Lesson[];
}
