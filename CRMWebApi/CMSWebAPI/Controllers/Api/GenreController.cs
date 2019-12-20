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
    [Route("api/genre")]
    public class GenreController : APIControllerInitializer
    {
        public GenreController(CMSWebAPIDbModelContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {


        }
        
        [HttpPost]
        [Route("creategenre")]
        [HandleAccess(UserType.Admin)]
        public object CreateGenre([FromBody]GenreModel genremodel)
        {
            try
            {
                if(genremodel.NovelId == 0)
                {
                    throw new Exception("Novel Id is required!");
                }

                if (string.IsNullOrEmpty(genremodel.Title))
                {
                    throw new Exception("Genre Title is required!");
                }

                var genreToSave = new Genre();
                using (var genreCommand = new GenreCommand(_context, false))
                {
                    genreToSave.NovelId = genremodel.NovelId;
                    genreToSave.Title = genremodel.Title;
                    genreCommand.Insert(genreToSave);
                    genreCommand.Commit();
                }
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseMessage = "Genre created for novel!"
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
        [Route("deletegenre")]
        [HandleAccess(UserType.Admin)]
        public object DeleteGenre([FromBody]GenreDeleteModel genremodel)
        {
            try
            {
                var genreToDelete = new Genre();
                using (var genreCommand = new GenreCommand(_context, false))
                {
                   var generesToDelete=genreCommand.Get(genremodel.NovelId,genremodel.GenreTitle);
                    foreach (var genereToDelete in generesToDelete)
                    {
                        genreCommand.Delete(genereToDelete);                       
                    }
                    genreCommand.Commit();
                }
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseMessage = "Genre Deleted for novel!"
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
        [Route("getallgenres")]
        public object GetAllGenres()
        {
            try
            {
                var allGenres = new List<GetGenreModel>();
                using (var genreCommand = new GenreCommand(_context, false))
                {
                    allGenres = genreCommand.GetAllGenres().Select(genre => new GetGenreModel
                    {
                        Id = genre.Id,
                        Title = genre.Title
                    }).ToList();
                }
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseData = allGenres,
                    ResponseMessage = "All Genres Fetched!"
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
        [Route("getgenressearchedbyuser")]
        public object GetGenresSearchedbyUser([FromBody]GenreSearchTitleModel genresearchname)
        {
            try
            {
                if (string.IsNullOrEmpty(genresearchname.Title))
                {
                    throw new Exception("Genre Title is required!");
                }

                var genrelist = new List <GetGenreModel>();
                using (var novelCommand = new NovelCommand(_context, false))
                {
                    genrelist = novelCommand.Get(genresearchname.Title).Select(novel => new GetGenreModel
                    {
                        Id = novel.Id,
                        Title = novel.Name
                    }).ToList();
                }
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseStatus.Success,
                    ResponseData = genrelist,
                    ResponseMessage = "Genre List Fetched!"
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