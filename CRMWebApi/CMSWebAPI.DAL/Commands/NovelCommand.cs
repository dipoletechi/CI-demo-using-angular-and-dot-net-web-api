using System;
using System.Linq;
using CMSWebAPI.Models.DbModels;

namespace CMSWebAPI.DAL.Commands
{
    public class NovelCommand : Command<Novel>, IDisposable
    {

        public NovelCommand(CMSWebAPIDbModelContext _dbContext)
        {
            DbContext = _dbContext;
        }

        public NovelCommand(CMSWebAPIDbModelContext _dbContext, bool isDispose = true)
        {
            IsDisposable = isDispose;
            DbContext = _dbContext;
        }

        public IQueryable <Novel> GetAllNovels()
        {
            return DbSet;
        }


        public IQueryable<Novel> Get(string keyword)
        {
            return DbSet.Where(n=>n.Genres.Any(g=>g.Title.Contains(keyword)));
        }

        public void Dispose()
        {
            if (IsDisposable)
            {
                DbContext.Dispose();
            }
        }
    }
}
