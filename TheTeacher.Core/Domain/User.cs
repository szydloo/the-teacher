using System;
using System.Net.Mail;
using TheTeacher.Core.Exceptions;

namespace TheTeacher.Core.Domain
{
    public class User
    {
        public Guid UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Fullname { get; protected set; }
        public string Role { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        
        protected User()
        {
        }
        
        public User(string email, string password, string salt, string username, string fullname, string role)
        {
            UserId = Guid.NewGuid();
            SetName(username);
            SetEmail(email);
            SetPassword(password);
            SetFullname(fullname);
            Salt = salt;
            Role = role;
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public bool IsValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void SetEmail(string email)
        {
            if(IsValid(email))
            {
                Email = email;
                UpdatedAt = DateTime.UtcNow;
            }
            else throw new DomainException(DomainErrorCodes.InvalidEmail, "Invalid email adress.");
        }

        private void SetPassword(string password)
        {
            if( String.IsNullOrWhiteSpace(password))
            {
                throw new DomainException(DomainErrorCodes.InvalidPassword, "Password cannot be empty.");
            }
            else if( password == Password)
            {
                return;
            }
            
            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetName(string name)
        {
            if( String.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(DomainErrorCodes.InvalidUsername, "Username cannot be empty.");
            }
            else if (name.Length < 2)
            {
                throw new DomainException(DomainErrorCodes.InvalidUsername, "Username has to have at least 2 characters.");
            }
            else if( name == Username)
            {
                return;
            }

            Username = name;
            UpdatedAt = DateTime.UtcNow;            
        }

        public void SetFullname(string fullname)
        {
            if( String.IsNullOrWhiteSpace(fullname))
            {
                throw new DomainException(DomainErrorCodes.InvalidName, "Fullname cannot be empty.");
            }
            else if (fullname.Length < 2)
            {
                throw new DomainException(DomainErrorCodes.InvalidName, "Fullname has to have at least 2 characters.");
            }
            else if( fullname == Fullname)
            {
                return;
            }

            Fullname = fullname;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}