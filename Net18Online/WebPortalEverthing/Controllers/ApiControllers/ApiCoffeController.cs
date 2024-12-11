using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiCoffeController : ControllerBase
    {
        private IKeyCoffeShopRepository _coffeShopRepository;
        private AuthService _authService;

        public ApiCoffeController(IKeyCoffeShopRepository coffeShopRepository, AuthService authService)
        {
            _coffeShopRepository = coffeShopRepository;
            _authService = authService;
        }

        public bool UpdateCoffe(int id, string name)
        {
            Thread.Sleep(1 * 1000);

            _coffeShopRepository.UpdateCoffeName(id, name);
            
            return true;
        }

        public bool Delete(int id)
        {
            _coffeShopRepository.Delete(id);
            return true;
        }

    }
}
