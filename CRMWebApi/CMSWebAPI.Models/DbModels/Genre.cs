using System.ComponentModel.DataAnnotations;

namespace CMSWebAPI.Models.DbModels
{
    public class Genre:Entity
    {
        public virtual Novel Novel { get; set; }

        [Required]
        public int NovelId { get; set; }
        public string Title { get; set; }
    }
}
