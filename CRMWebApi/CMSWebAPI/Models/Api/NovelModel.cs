using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSWebAPI.Models.Api
{
    public class NovelModel
    {
        public string Name { get; set; }
    }

    public class GetNovelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
