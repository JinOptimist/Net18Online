using Everything.Data.Repositories.Surveys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Surveys;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private IDocumentRepositoryReal _documentRepository;
        private FileProvider _fileProvider;

        public DocumentsController(IDocumentRepositoryReal documentRepository, FileProvider fileProvider)
        {
            _documentRepository = documentRepository;
            _fileProvider = fileProvider;
        }

        public IActionResult Index()
        {
            var documents = _documentRepository.GetAll();

            var viewModel = new DocumentIndexViewModel()
            { 
                Documents = documents.Select(document => new DocumentViewModel()
                {
                    Id = document.Id,
                    Title = document.Title,
                    OriginalFileName = document.OriginalFileName,
                    Length = document.Length
                })
                .ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new DocumentCreateOrEditViewModel()
            {
                Id = 0
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(DocumentCreateOrEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var newFileName = _fileProvider.GetNewFileName(viewModel.FormFile.FileName);

            if (!_fileProvider.Save(viewModel.FormFile, newFileName))
            {
                ModelState.AddModelError(
                    nameof(DocumentCreateOrEditViewModel.FormFile),
                    "Не удалось сохранить файл, обратитесь к администратору портала");

                return View(viewModel);
            }

            _documentRepository.Create(viewModel.Title, viewModel.FormFile.FileName, newFileName, viewModel.FormFile.ContentType, viewModel.FormFile.Length);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _documentRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
