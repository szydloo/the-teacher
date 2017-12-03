using System;

namespace TheTeacher.Core.Domain
{
    public class User
    {
        public Guid UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Fullname { get; protected set; }
        public string Role { get; protected set; }
        
        protected User()
        {

        }
        public User(string username,string email, string password, string role, string fullname)
        {
            SetName(username);
            SetEmail(email);
            SetPassword(password);
            SetFullname(fullname);
            Role = role;
        }

        private void SetEmail(string email)
        {
            if(String.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email cannot be empty");
            }
            else if (email == Email)
            {
                return;
            }
            else if( email.Length < 5 || !email.Contains("@"))
            {
                throw new Exception("Invalid email format")
            }
            Email = email;
        }

        private void SetPassword(string password)
        {
            if( String.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot be empty.");
            }
            else if (password.Length < 6 )
            {
                throw new Exception("Password has to have at least 6 characters.");
            }
            else if( password == Password)
            {
                return;
            }
            else 
            {
                Password = password;
            }
        }

        private void SetName(string name)
        {
            if( String.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Username cannot be empty.");
            }
            else if (name.Length < 2)
            {
                throw new Exception("Username has to have at least 2 characters.");
            }
            else if( name == Username)
            {
                return;
            }
            else 
            {
                Username = name;
            }
        }

        public void SetFullname(string fullname)
        {
            if( String.IsNullOrWhiteSpace(fullname))
            {
                throw new Exception("Fullname cannot be empty.");
            }
            else if (fullname.Length < 2)
            {
                throw new Exception("Fullname has to have at least 2 characters.");
            }
            else if( fullname == Fullname)
            {
                return;
            }
            else 
            {
                Fullname = fullname;
            }
        }
    }
}