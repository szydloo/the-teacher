using System;
using System.Net.Mail;
using TheTeacher.Core.Exceptions;

namespace TheTeacher.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Role { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public PersonalDetails Details { get; protected set; }
        
        protected User()
        {
        }
        
        public User(Guid id, string email, string password, string salt, string username, string role)
        {
            Id = id;
            SetUsername(username);
            SetEmail(email);
            SetPassword(password);
            Salt = salt;
            SetRole(role);
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        private void SetRole(string role)
        {
            if(role == null)
            {
                Role = "user";
            }
            else 
            {
                Role = role;
            }
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
            else throw new DomainException(DomainErrorCodes.InvalidEmail, "Invalid email address.");
        }

        public void SetPassword(string password)
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

        private void SetUsername(string username)
        {
            if( String.IsNullOrWhiteSpace(username))
            {
                throw new DomainException(DomainErrorCodes.InvalidUsername, "Username cannot be empty.");
            }
            else if (username.Length < 2)
            {
                throw new DomainException(DomainErrorCodes.InvalidUsername, "Username has to have at least 2 characters.");
            }
            else if( username == Username)
            {
                return;
            }

            Username = username;
            UpdatedAt = DateTime.UtcNow;            
        }
    }
}