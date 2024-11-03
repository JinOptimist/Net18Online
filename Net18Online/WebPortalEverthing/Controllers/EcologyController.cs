using Everything.Data;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Everything.Data.Repositories;
using WebPortalEverthing.Models.Ecology;
using Everything.Data.Models;



namespace WebPortalEverthing.Controllers
{
    public class EcologyController : Controller
    {
        private IEcologyRepositoryReal _ecologyRepository;
        private WebDbContext _webDbContext;

        public EcologyController(IEcologyRepositoryReal ecologyRepository, WebDbContext webDbContext)
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
            var ecologyFromDb = _ecologyRepository.GetAll();

            var ecologyViewModels = ecologyFromDb
                .Select(dbEcology =>
                    new EcologyViewModel
                    {
                        Id = dbEcology.Id,
                        ImageSrc = dbEcology.ImageSrc,
                        Texts = new List<string>()
                    }
                )
                .ToList();
            return View(ecologyViewModels);
        }

        [HttpPost]
        public IActionResult EcologyChat(PostCreationViewModel viewModel)
        {
            var ecology = new EcologyData
            {
                ImageSrc = viewModel.Url,
                //Text = new List<string>{viewModel.Text},
            };
            _ecologyRepository.Add(ecology);
            
            return RedirectToAction("EcologyChat");
        }
        
        /*public IActionResult UpdateText(string newText, int id)
        {
            _ecologyRepository.UpdateText(id, newText);
            return RedirectToAction("EcologyChat");
        }*/

        [HttpPost]
        public IActionResult UpdateImage(int id, string url)
        {
            _ecologyRepository.UpdateImage(id, url);
            return RedirectToAction("EcologyChat");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _ecologyRepository.Delete(id);
            return RedirectToAction("EcologyChat");
        }

        /*[HttpPost]
        public IActionResult UpdatePost(int id, string url, string text)
        {
            _ecologyRepository.UpdatePost(id, url, text); 
            return RedirectToAction("EcologyChat");
        }*/
    }
}