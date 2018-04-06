namespace TheTeacher.Infrastructure.Exceptions
{
    public class ServiceErrorCodes
    {
        public static string EmailInUse => "email_in_use";
        public static string TeacherAlreadyExists => "teacher_already_exists";
        public static string UserAlreadyExists => "user_already_exists";        
        public static string TeacherNotFound => "teacher_not_found";
        public static string UserNotFound => "user_not_found";
        public static string InvalidCredentials => "invalid_credentials";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidSubjectDetails => "invalid_subject_details";
        public static string InvalidNewPassword => "invalid_new_password";
    }
}