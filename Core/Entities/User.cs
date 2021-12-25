using Core.Enums;
using Core.Exceptions;
using System;

namespace Core.Entities
{
    public abstract class User : BaseEntity
    {
        #region protected properties

        protected string _username;
        protected string _password;
        protected ROLE _role;

        #endregion

        #region public properties

        public string Username { get => _username; }
        public string Password { get => _password; }
        public ROLE Role { get => _role; }

        #endregion

        #region constructor

        public User(Guid id, string username, string password, ROLE role) 
        {
            _id = id;
            _username = username;
            _password = password;
            _role = role;            
        }

        #endregion

        #region protected members

        protected virtual void Validate()
        {
            
            if (String.IsNullOrWhiteSpace(_username))
            {
                _validationErrors.Add("Username", "The Username field can not be empty, null or white spaced");
            }
            if (String.IsNullOrWhiteSpace(_password))
            {
                _validationErrors.Add("Password", "The Password field can not be empty, null or white spaced");
            }
            
        }

        #endregion
    }
}
