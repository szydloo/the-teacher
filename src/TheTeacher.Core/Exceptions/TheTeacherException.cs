using System;

namespace TheTeacher.Core.Exceptions
{
    public abstract class TheTeacherException : Exception
    {
        public string Code { get; }

        protected TheTeacherException()
        {
        }

        public TheTeacherException(string code)
        {
            Code = code;
        }

        public TheTeacherException(string message, params object[] args)  : this(string.Empty, message, args)
        {
        }

        public TheTeacherException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public TheTeacherException(Exception innerException, string message, params object[] args)
             : this(innerException, string.Empty, message, args)
        {
        }

        public TheTeacherException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}