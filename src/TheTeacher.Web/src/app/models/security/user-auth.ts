import { PersonalDetails } from "../personal-details";

// TODO: Add custom claims?
export class UserAuth {
    userId: string;
    token = '';
    isAuthenticated = false;
    isTeacher = false;
    role = '';
    username = '';
}
