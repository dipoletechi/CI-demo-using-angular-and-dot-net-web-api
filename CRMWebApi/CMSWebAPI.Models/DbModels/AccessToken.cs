using System.ComponentModel.DataAnnotations;

namespace CMSWebAPI.Models.DbModels
{
    public class AccessToken : Entity
    {
        public string Token { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
