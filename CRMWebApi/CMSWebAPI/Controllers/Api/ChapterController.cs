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
    [Route("api/chapter")]
    public class ChapterController : APIControllerInitializer
    {
        public ChapterController(CMSWebAPIDbModelContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {


        }

        [HttpPost]
        [Route("createchapter")]
        [HandleAccess(UserType.Admin)]
        public object CreateChapter([FromBody]ChapterModel chaptermodel)
        {
            try
            {
                if (chaptermodel.NovelId == 0)
                {
                    throw new Exception("Novel Id is required!");
                }

                if (string.IsNullOrEmpty(chaptermodel.Name))
                {
                    throw new Exception("Chapter Name is required!");
                }

                if (string.IsNullOrEmpty(chaptermodel.Content))
                {
                    throw new Exception("Chapter Content is required!");
                }

                var chapterToSave = new Chapter();
                using (var chapterCommand = new ChapterCommand(_context, false))
                {
                    chapterToSave.NovelId = chaptermodel.NovelId;
                    chapterToSave.Name = chaptermodel.Name;
                    chapterToSave.Content = chaptermodel.Content;
                    chapterToSave.IsActive = false;
                    chapterCommand.Insert(chapterToSave);
                    chapterCommand.Commit();
                }

                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseMessage = "Chapter created in Novel"
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
        [Route("getallchapters")]
        public object GetAllChapters()
        {
            try
            {
                var allChapters = new List<GetChapterModel>();
                using (var chapterCommand = new ChapterCommand(_context, false))
                {
                    allChapters = chapterCommand.GetAllChapters().Select(chapter => new GetChapterModel
                    {
                        Id = chapter.Id,
                        Name = chapter.Name
                    }).ToList();
                }
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseData = allChapters,
                    ResponseMessage = "All Chaptes Fetched!"
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

        [HttpPost]
        [Route("publishchapter")]
        [HandleAccess(UserType.Admin)]
        public object PublishChapter([FromBody]PublishChapterModel publishchaptermodel)
        {
            try
            {
                if (publishchaptermodel.Id == 0)
                {
                    throw new Exception("Id is required!");
                }
                
                using (var chapterCommand = new ChapterCommand(_context, false))
                {
                    var chapterToPublish = chapterCommand.Get(publishchaptermodel.Id);
                    chapterToPublish.IsActive = true;
                    chapterCommand.Update(chapterToPublish);
                    chapterCommand.Commit();
                }

                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseMessage = "Chapter Published!"
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