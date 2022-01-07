using Core.Entities;
using Helpers;

namespace DataLayer.Extensions
{
    public static class OwnerEntityExtensions
    {
        #region public static methods

        public static object OwnerEntityToDBModel(this Owner owner)
        {
            return new
            {
                owner.Id,
                owner.Email,
                owner.Username,
                Password = Security.GenerateMD5(owner.Password),
                owner.Role                

            };
        }

        #endregion
    }
}
