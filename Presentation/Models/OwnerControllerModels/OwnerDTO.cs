using Core.Entities;
using Core.Enums;
using System;

namespace BikesAnonymous.Models.OwnerControllerModels
{
    public class OwnerDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    
        
        public Owner ToDomain()
        {
            return new Owner(Guid.NewGuid(), Username, Password, ROLE.OWNER, Email);
           
        }
    
    }

}
