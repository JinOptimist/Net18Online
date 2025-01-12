using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Localizations;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiLoadChatController : ControllerBase
    {
        private ILoadChatMessageRepositryReal _loadChatMessageRepositry;

        public ApiLoadChatController(ILoadChatMessageRepositryReal loadChatMessageRepositry)
        {
            _loadChatMessageRepositry = loadChatMessageRepositry;
        }

        public List<string> GetLastMessages()
        {
            return _loadChatMessageRepositry.GetLastMessages();
        }
    }
}
