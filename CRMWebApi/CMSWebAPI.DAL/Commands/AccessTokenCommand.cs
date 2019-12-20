using CMSWebAPI.Models.DbModels;
using System;
using System.Linq;

namespace CMSWebAPI.DAL.Commands
{
    public class AccessTokenCommand : Command<AccessToken>, IDisposable
    {

        public AccessTokenCommand(CMSWebAPIDbModelContext _dbContext)
        {
            DbContext = _dbContext;
        }

        public AccessTokenCommand(CMSWebAPIDbModelContext _dbContext, bool isDispose = true)
        {
            IsDisposable = isDispose;
            DbContext = _dbContext;
        }

        public AccessToken Get(string accessToken)
        {
            return DbSet.FirstOrDefault(p => p.Token == accessToken);
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

