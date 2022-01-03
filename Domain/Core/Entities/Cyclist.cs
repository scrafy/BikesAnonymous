using Core.Enums;
using Core.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace Core.Entities
{
    public class Cyclist : User
    {
        #region private properties

        private DateTime _licenseRegistrationDate;
        private DateTime _licenseExpirationDate;
        private Guid _licenseCode;
        private string _firstName;
        private string _lastName;
        private Int16 _age;
        private string _email;

        #endregion

        #region public properties

        public DateTime LicenseRegistrationDate { get => _licenseRegistrationDate;  }
        public DateTime LicenseExpirationDate { get => _licenseExpirationDate;  }
        public Guid LicenseCode { get => _licenseCode; }
        public string FirstName { get => _firstName;  }
        public string LastName { get => _lastName; }
        public short Age { get => _age; }
        public string Email { get => _email; }

        #endregion

        #region constructor

        public Cyclist(

            Guid id, 
            string username, 
            string password, 
            ROLE role,
            DateTime licenseRegistrationDate,
            DateTime licenseExpirationDate,
            Guid licenseCode,
            string firstName,
            string lastName,
            short age,
            string email

            ) : base(id, username, password, role)
        {
            _licenseRegistrationDate = licenseRegistrationDate;
            _licenseExpirationDate = licenseExpirationDate;
            _licenseCode = licenseCode;
            _firstName = firstName;
            _lastName = lastName;
            _age = age;
            _email = email;
            Validate();
        }

        #endregion

        #region protected members

        protected override void Validate()
        {
            base.Validate();
            
          
          if (_role != ROLE.CYCLIST)
           {
                _validationErrors.Add("Role", $"Incorrect role {_role.ToString()}");
           }           
           if (_licenseRegistrationDate >= _licenseExpirationDate)
                _validationErrors.Add("LicenseRegistrationDate", "The LicenseRegistrationDate has to be greater than LicenseExpirationDate");

           if (String.IsNullOrWhiteSpace(_firstName))
            {
                _validationErrors.Add("FirstName", "The FirstName field can not be empty, null or white spaced");
            }
            if (String.IsNullOrWhiteSpace(_lastName))
            {
                _validationErrors.Add("LastName", "The LastName field can not be empty, null or white spaced");
            }
            if( _age <= 0)
                _validationErrors.Add("Age", "The Age field can not be zero");

            if (!new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(_email).Success)
                _validationErrors.Add("Email", "The email address has an invalid format");

            if (_validationErrors.Count > 0) 
                throw new ValidationException("It was not possible to instantiate a Cyclist model", _validationErrors, ErrorCode.BAD_REQUEST);

          
        }

        #endregion


    }
}
