using System.ComponentModel.DataAnnotations;

namespace CMSWebAPI.Models.DbModels
{
    public class Chapter:Entity
    {
        public virtual Novel Novel { get; set; }

        [Required]
        public int NovelId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
