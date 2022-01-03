using Core.Enums;
using Core.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace Core.Entities
{
    public class Owner : User
    {

        #region private properties
             
        private string _email;

        #endregion

        #region public properties

        public string Email { get => _email; }

        #endregion

        #region constructor

        public Owner(Guid id, string username, string password, ROLE role, string email) : base(id, username, password, role)
        {
            _id = id;
            _username = username;
            _password = password;
            _role = role;
            _email = email;
            Validate();
        }

        #endregion

        #region protected members

        protected override void Validate()
        {
            base.Validate();

            if (!new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(_email).Success)
                _validationErrors.Add("Email", "The email address has an invalid format");

            if (_validationErrors.Count > 0)
                throw new ValidationException("It was not possible to instantiate a Owner model", _validationErrors, ErrorCode.BAD_REQUEST);
        }

        #endregion
    }
}
