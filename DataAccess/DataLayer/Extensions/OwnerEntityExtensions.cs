using Core.Entities;
using Helpers;

namespace DataLayer.Extensions
{
    public static class CyclistEntityExtensions
    {
        #region public static methods

        public static object CyclistEntityToDBModel(this Cyclist cyclist)
        {
            return new
            {
                cyclist.Id,
                cyclist.Email,
                cyclist.Username,
                Password = Security.GenerateMD5(cyclist.Password),
                cyclist.Role,
                cyclist.Age,
                cyclist.FirstName,
                cyclist.LastName,
                cyclist.LicenseCode,
                cyclist.LicenseExpirationDate,
                cyclist.LicenseRegistrationDate               

            };
        }

        #endregion
    }
}
