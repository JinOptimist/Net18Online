using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebPortalEverthing.Services.LoadTesting;
using WebPortalEverthing.Models.LoadTesting;
using WebPortalEverthing.Models.LoadTesting.LoadTestingApi;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    //  [Route("api/methods")]
    //  [Route("/ApiExplorer")]
    public class ApiExplorerController : Controller
    {
        private readonly ApiReflectionService _reflectionService;

        public ApiExplorerController(ApiReflectionService reflectionService)
        {
            _reflectionService = reflectionService;
        }

        /*   [HttpGet]  нужен, чтобы отобразить страницу Api методов, перед заполнением полей и отправки запроса */
        [HttpGet]
        public IActionResult ApiExplorerView()
        {
            var viewModel = _reflectionService.GetApiMethods();
            return View(viewModel);
        }

        /* HttpPost  нужен, чтобы послать данные заполненные пользователем в экшен т.е. запрос  */
     /*   [HttpPost]
        public IActionResult SendRequest(string url, string method, Dictionary<string, object> parameters)
        {
            using (var client = new HttpClient())
            {
                // Формируем запрос
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = new HttpMethod(method),
                    RequestUri = new Uri($"https://localhost:7130{url}")
                };

                if (method != "GET" && parameters != null)
                {
                    request.Content = new StringContent(
                        Newtonsoft.Json.JsonConvert.SerializeObject(parameters),
                        Encoding.UTF8,
                        "application/json"
                    );
                }

                // Отправляем запрос
                var response = client.Send(request);
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //   return Content(responseContent);
            }
            return RedirectToAction("SendRequest", "ApiExplorer");
        } не работает */





    }

}
