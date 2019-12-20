using System.Collections.Generic;

namespace CMSWebAPI.Models.DbModels
{
    public class Novel:Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}
