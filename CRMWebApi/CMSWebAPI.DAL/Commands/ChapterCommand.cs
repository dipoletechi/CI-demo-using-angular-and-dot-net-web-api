using System;
using System.Linq;
using CMSWebAPI.Models.DbModels;

namespace CMSWebAPI.DAL.Commands
{
    public class ChapterCommand : Command<Chapter>, IDisposable
    {
        public ChapterCommand(CMSWebAPIDbModelContext _dbContext)
        {
            DbContext = _dbContext;
        }

        public ChapterCommand(CMSWebAPIDbModelContext _dbContext, bool isDispose = true)
        {
            IsDisposable = isDispose;
            DbContext = _dbContext;
        }

        public IQueryable<Chapter> GetAllChapters()
        {
            return DbSet;
        }

        public Chapter Get(int id)
        {
            return DbSet.FirstOrDefault(chap=>chap.Id==id);
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
