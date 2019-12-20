using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSWebAPI.Models.Api
{
    public class GenreModel
    {
        public int NovelId { get; set; }
        public string Title { get; set; }
    }

    public class GetGenreModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class GenreSearchTitleModel
    {
        public string Title { get; set; }
    }

    public class GenreDeleteModel
    {
        public int NovelId { get; set; }
        public string GenreTitle { get; set; }
    }
}
