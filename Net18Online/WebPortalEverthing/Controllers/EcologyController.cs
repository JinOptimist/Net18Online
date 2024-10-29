using Everything.Data;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Ecology;

namespace WebPortalEverthing.Controllers
{
    public class EcologyController : Controller
    {
        private IEcologyRepository _ecologyRepository;
        private WebDbContext _webDbContext;

        public EcologyController(IEcologyRepository ecologyRepository,  WebDbContext webDbContext)
        {
            _ecologyRepository = ecologyRepository;
            _webDbContext = webDbContext;
        }

        public IActionResult Index()
        {
            var model = new EcologyViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult EcologyChat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EcologyChat(PostCreationViewModel viewModel)
        {
            var dataEcology = new EcologyData
            {
                ImageSrc = viewModel.Url,
                //Text = new List<string>{viewModel.Text},
            };
            _webDbContext.Ecology.Add(dataEcology);
            _webDbContext.SaveChanges();

            var ecologyFromRealDb = _ecologyRepository.GetAll();

            var ecologyViewModels = ecologyFromRealDb
                .Select(dbEcology =>
                    new EcologyViewModel
                    {
                        ImageSrc = dbEcology.ImageSrc,
                        //Texts = dbEcology.Text
                    }
                )
                .ToList();

            return View(ecologyViewModels);
        }
    }
}