using System;
using System.Linq;
using CMSWebAPI.DbModels.Enums;
using CMSWebAPI.Models.DbModels;

namespace CMSWebAPI.DAL.Commands
{
    public class UserCommand : Command<User>, IDisposable
    {

        public UserCommand(CMSWebAPIDbModelContext _dbContext)
        {
            DbContext = _dbContext;
        }
      

        public UserCommand(CMSWebAPIDbModelContext _dbContext, bool isDispose = true)
        {
            IsDisposable = isDispose;
            DbContext = _dbContext;
        }

        public void Dispose()
        {
            if (IsDisposable)
            {
                DbContext.Dispose();
            }
        }

        public User Get(string email)
        {
            return DbSet.FirstOrDefault(u =>u.Email==email);
        }

        public User GetUserIdByAccessToken(string token)
        {
            return DbSet.FirstOrDefault(u => u.AccessTokens.Any(at => at.Token == token));
        }

        public User GetUserByAccessTokenAndType(string token,UserType userType)
        {
            return DbSet.FirstOrDefault(u => u.AccessTokens.Any(at => at.Token == token) && u.UserType== userType);
        }
    }

}

