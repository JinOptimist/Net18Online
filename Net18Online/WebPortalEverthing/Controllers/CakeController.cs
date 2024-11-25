using Everything.Data;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Cake;
using WebPortalEverthing.Models.Magazin;

namespace WebPortalEverthing.Controllers
{
    public class CakeController : Controller
    {
        private ICakeRepositoryReal _cakeRepository;
        private WebDbContext _webDbContext;

        public CakeController(ICakeRepositoryReal cakeRepository, WebDbContext webDbContext)
        {
            _cakeRepository = cakeRepository;
            _webDbContext = webDbContext;
        }
        public IActionResult Index()
        {
            var cakesFromDb = _webDbContext
                .Cakes;

            var cakesViewModel = cakesFromDb
                .Select(dbCake => new CakeViewModel
                {
                    ImageSrc = dbCake.ImageSrc,
                    Name = dbCake.Name,
                    Description = dbCake.Description,
                    Price = dbCake.Price,
                    Magazins = dbCake.Magazins
                                .Select(dbMagazin => new MagazinViewModel
                                {
                                    Name = dbMagazin.Name,
                                })
                                .ToList(),
                }).ToList();

            return View(cakesViewModel);
        }
    }
}
