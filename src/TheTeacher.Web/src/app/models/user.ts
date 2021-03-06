import { PersonalDetails } from "./personal-details";

export interface User {
    email: string;
    username: string;
    password: string;
    role: string;
    details: PersonalDetails;
}
