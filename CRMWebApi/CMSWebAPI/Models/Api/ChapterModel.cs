using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSWebAPI.Models.Api
{
    public class ChapterModel
    {
        public int NovelId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class GetChapterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PublishChapterModel
    {
        public int Id { get; set; }
    }
}
