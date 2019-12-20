using System;
using System.Collections.Generic;
using System.Linq;
using CMSWebAPI.DAL;
using CMSWebAPI.DAL.Commands;
using CMSWebAPI.DbModels.Enums;
using CMSWebAPI.Filters;
using CMSWebAPI.Models;
using CMSWebAPI.Models.Api;
using CMSWebAPI.Models.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMSWebAPI.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/novel")]
    public class NovelController : APIControllerInitializer
    {
        public NovelController(CMSWebAPIDbModelContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {


        }

        [HttpPost]
        [Route("createnovel")]
        [HandleAccess(UserType.Admin)]
        public object CreateNovel([FromBody]NovelModel novelmodel)
        {
            try
            {
                if (string.IsNullOrEmpty(novelmodel.Name))
                {
                    throw new Exception("Novel Name is required!");
                }

                var novelToSave = new Novel();
                using (var novelCommand = new NovelCommand(_context, false))
                {
                    novelToSave.Name = novelmodel.Name;
                    novelCommand.Insert(novelToSave);
                    novelCommand.Commit();
                }                                        
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseMessage = "Novel created!"
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

        [HttpGet]
        [Route("getavailablenovels")]
        public object GetAvailableNovels()
        {
            try
            {
                var allNovels = new List <GetNovelModel>();
                using (var novelCommand = new NovelCommand(_context, false))
                {
                    allNovels = novelCommand.GetAllNovels().Select(novel => new GetNovelModel
                    {
                        Id = novel.Id,
                        Name = novel.Name
                    }).ToList();
                }
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseData = allNovels,
                    ResponseMessage = "All Novels Fetched!"
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
