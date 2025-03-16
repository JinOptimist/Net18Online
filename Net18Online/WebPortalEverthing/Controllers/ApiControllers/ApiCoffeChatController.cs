using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebPortalEverthing.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiCoffeChatController : ControllerBase
    {
        private ICoffeChatMessageRepositryKey _coffeChatMessageRepositryKey;

        public ApiCoffeChatController(ICoffeChatMessageRepositryKey coffeChatMessageRepositryKey)
        {
            _coffeChatMessageRepositryKey = coffeChatMessageRepositryKey;
        }

        [HttpGet]
        public IActionResult GetMessages(int pageNumber = 1, int pageSize = 4)
        {
            var messages = _coffeChatMessageRepositryKey.GetAllMessages(pageNumber, pageSize);
            return Ok(messages);
        }
    }
}
