using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CMSWebAPI.DAL;
using CMSWebAPI.Helpers;

namespace CMSWebAPI.Models
{
    public class APIControllerInitializer : Controller
    {
        protected CMSWebAPIDbModelContext _context;
        protected IHostingEnvironment _hostingEnvironment;
        protected UserInfo UserInfo;        

        public APIControllerInitializer(CMSWebAPIDbModelContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            Initialize(httpContextAccessor.HttpContext.Session);
        }      
        
        protected void Initialize(ISession session)
        {
            UserInfo = SessionExtensionHelper.Get<UserInfo>(session, "userinfo");            
        }
    }    
}
