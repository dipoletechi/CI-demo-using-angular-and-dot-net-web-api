using System;
using System.ComponentModel.DataAnnotations;

namespace CMSWebAPI.Models
{
   public class Entity
    {
       public Entity()
        {
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            IsActive = true;
            IsDeleted = false;
            IsArchived = false;
        }
        [Required]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }
    }
}
