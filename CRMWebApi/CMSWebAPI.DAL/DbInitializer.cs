using System.Collections.Generic;

using CMSWebAPI.DAL.Commands;
using CMSWebAPI.DAL.Utils;
using CMSWebAPI.Models.DbModels;
using CMSWebAPI.DbModels.Enums;

namespace CMSWebAPI.DAL.Global
{
    public static class DbInitializer
    {

        public static void Initialize(CMSWebAPIDbModelContext context)
        {

            context.Database.EnsureCreated();
   
            #region Seed User
            var users = new List<User>();

            using (var userCommand = new UserCommand(context, false))
            {
                if (!userCommand.Any())
                {


                    var passwordSalt = PasswordUtil.GetSalt();
                    var hashedPassword = PasswordUtil.GetHash("Dipole@1", passwordSalt);
                  
                    users.Add(new User
                    {                                             
                        FirstName = "Admin",
                        LastName = "Admin",                        
                        Email = "admin@wuxiaworld.com",
                        SaltedPassword = hashedPassword,
                        UserType = UserType.Admin,
                        Salt = passwordSalt                    
                    });

                    users.Add(new User
                    {                        
                        FirstName = "admin",
                        LastName = "Admin",                       
                        Email = "consumer@wuxiaworld.com",
                        UserType = UserType.Consumer,
                        SaltedPassword = hashedPassword,
                        Salt = passwordSalt                      
                    });                    
                    userCommand.Insert(users);
                    userCommand.Commit();
                }

            }
            #endregion
            
        }
    }


}
