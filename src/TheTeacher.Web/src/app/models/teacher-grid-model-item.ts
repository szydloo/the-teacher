import { PersonalDetails } from "./personal-details";
import { Lesson } from "./lesson";

export class TeacherGridModelItem {
    email: string;
    details: PersonalDetails;
    lessons: Lesson[];
}