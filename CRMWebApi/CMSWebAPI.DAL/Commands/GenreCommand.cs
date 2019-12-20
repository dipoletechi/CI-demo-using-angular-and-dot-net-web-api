using System;
using System.Linq;
using CMSWebAPI.Models.DbModels;

namespace CMSWebAPI.DAL.Commands
{
    public class GenreCommand : Command<Genre>, IDisposable
    {
        public GenreCommand(CMSWebAPIDbModelContext _dbContext)
        {
            DbContext = _dbContext;
        }

        public GenreCommand(CMSWebAPIDbModelContext _dbContext, bool isDispose = true)
        {
            IsDisposable = isDispose;
            DbContext = _dbContext;
        }

        public IQueryable<Genre> Get(int Id,string Title)
        {
            return DbSet.Where(g => g.NovelId == Id && g.Title.ToLower()==Title.ToLower());
        }

        public IQueryable<Genre> GetAllGenres()
        {
            return DbSet;
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
