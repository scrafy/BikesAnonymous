using Core.Entities;
using Core.Enums;
using Helpers;
using System;


namespace TraversalServices.Models
{
    public class CyclistDTO
    {
        #region public properties

        public string Username { get; set; }
        public string Password { get; set; }
        public ROLE Role { get; set; }
        public DateTime LicenseRegistrationDate { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public Guid LicenseCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Age { get; set; }
        public string Email { get; set; }

        #endregion


        #region public methods

        public Cyclist ToDomain()
        {
            return new Cyclist(

                         Guid.NewGuid(),
                         this.Username,
                         this.Password,
                         this.Role,
                         this.LicenseRegistrationDate,
                         this.LicenseExpirationDate,
                         this.LicenseCode,
                         this.FirstName,
                         this.LastName,
                         this.Age,
                         this.Email

            );
        }

        #endregion

    }
}
