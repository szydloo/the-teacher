export class ServiceErrorCodes {
    static readonly emailInUse: string = "email_in_use";
    static readonly teacherAlreadyExists: string = "teacher_already_exists";
    static readonly userAlreadyExists: string = "user_already_exists";        
    static readonly teacherNotFound: string = "teacher_not_found";
    static readonly userNotFound: string = "user_not_found";
    static readonly invalidCredentials: string = "invalid_credentials";
    static readonly invalidEmail: string = "invalid_email";
    static readonly invalidSubjectDetails: string = "invalid_subject_details";
    static readonly invalidNewPassword: string = "invalid_new_password";
}