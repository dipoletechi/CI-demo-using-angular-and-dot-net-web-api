using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using CMSWebAPI.Models.Api.Login;
using CMSWebAPI.DAL;
using CMSWebAPI.DAL.Commands;
using CMSWebAPI.DAL.Utils;
using CMSWebAPI.DbModels.Enums;
using CMSWebAPI.Models;
using CMSWebAPI.Models.DbModels;

namespace CMSWebAPI.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : APIControllerInitializer
    {
       
        public LoginController(CMSWebAPIDbModelContext context, IHttpContextAccessor httpContextAccessor):base(context, httpContextAccessor)
        {
            
        }

        [HttpPost]
        public object Post([FromBody]LoginModel login)
        {
            try
            {                
                var userId = 0;
                var loginResponse = new LoginDetails();
                using (var userCommand = new UserCommand(_context, false))
                {
                    var user = userCommand.Get(login.UserName);

                    if (user == null)
                    {
                        throw new Exception("Wrong credentials");
                    }
                   
                    var hashedPassword = PasswordUtil.GetHash(login.Password, user.Salt);
                    if (user.SaltedPassword != hashedPassword)
                    {
                        throw new Exception("Wrong credentials");
                    }

                    userId = user.Id;    
                    if(!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                    {
                        loginResponse.Name = user.FirstName + " " + user.LastName;
                    }
                    else
                    {
                        loginResponse.Name = user.FirstName;
                    }
                    
                }

                var accessToken = new AccessToken();

                #region Generate Access token                    
                using (var accessTokenCommand = new AccessTokenCommand(_context, false))
                {
                    accessToken = new AccessToken
                    {
                        Token = Guid.NewGuid().ToString(),
                        UserId = userId
                    };
                    accessTokenCommand.Insert(accessToken);
                    accessTokenCommand.Commit();
                }
                #endregion

                loginResponse.Token = accessToken.Token;
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseData = loginResponse,
                    ResponseMessage = "Welcome " + loginResponse.Name
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = ResponseStatus.Error,
                    ResponseData = ex.Message,
                    ResponseMessage = ex.Message
                };
            }

        }       
    }
}