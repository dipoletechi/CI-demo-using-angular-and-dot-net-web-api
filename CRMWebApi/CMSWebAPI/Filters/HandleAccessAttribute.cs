using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using CMSWebAPI.DAL;
using CMSWebAPI.DAL.Commands;
using CMSWebAPI.DbModels.Enums;
using CMSWebAPI.Helpers;
using CMSWebAPI.Models.DbModels;


namespace CMSWebAPI.Filters
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class HandleAccessAttribute : Attribute, IAuthorizationFilter
    {
        UserType? userType;
        public HandleAccessAttribute()
        {
            userType = null;
        }

        public HandleAccessAttribute(UserType _userType)
        {
            userType = _userType;
        }

        private User GetUserByAccessToken(string accessToken, CMSWebAPIDbModelContext _context,UserType? userType)
        {
           var user =new User();
            using (var userCommand = new UserCommand(_context, false))
            {
                if (userType == null)
                {
                    user = userCommand.GetUserIdByAccessToken(accessToken);
                }
                else
                {
                    user = userCommand.GetUserByAccessTokenAndType(accessToken,userType.Value);                    
                }
                
            }
            return user;
        }
      
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var authHeader = context.HttpContext.Request.Headers["Authorization"];
                var dbContext = (CMSWebAPIDbModelContext)context.HttpContext.RequestServices.GetService(typeof(CMSWebAPIDbModelContext));

                if (authHeader.Count > 0)
                {
                    var token = authHeader[0];
                    using (var accessToken = new AccessTokenCommand(dbContext, false))
                    {
                        var tokenObj = accessToken.Get(token);
                        if (tokenObj == null)
                        {
                            throw new Exception("Access token has been expired");
                        }
                        if (!tokenObj.IsActive || tokenObj.IsDeleted)
                        {
                            throw new Exception("Access token has been expired");
                        }
                    }

                    //if user has access token than get use details
                    var user = GetUserByAccessToken(token, dbContext, userType);

                    if (user == null)
                    {
                        throw new Exception("Token expired");
                    }
                    
                    var userinfo = new UserInfo();
                    userinfo.Email = user.Email;                    
                    userinfo.UserId = user.Id;
                    userinfo.FirstName = user.FirstName;
                    userinfo.LastName = user.FirstName;

                    if (!string.IsNullOrEmpty(user.LastName))
                    {
                        userinfo.Name = userinfo.FirstName + " " + user.LastName;
                    }
                    else
                    {
                        userinfo.Name = userinfo.FirstName;
                    }
                    userinfo.UserId = user.Id;

                    SessionExtensionHelper.Set<UserInfo>(context.HttpContext.Session, "userinfo", userinfo);
                }
                else
                {
                    throw new Exception("Access token has been expired");

                }
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.HttpContext.Response.Headers["Message"] = ex.Message;
                context.Result = new ContentResult { Content = ex.Message };
            }

        }
    }
}
