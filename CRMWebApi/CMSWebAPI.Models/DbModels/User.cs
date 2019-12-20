using System.Collections.Generic;

using CMSWebAPI.DbModels.Enums;

namespace CMSWebAPI.Models.DbModels
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }       
        public string Salt { get; set; }
        public string SaltedPassword { get; set; }        
        public UserType UserType { get; set; }        
        public virtual ICollection<AccessToken> AccessTokens { get; set; }                                       
    }
}

