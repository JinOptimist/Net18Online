using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeCatalog;
using TagGame.Classes.Builders;
using WebPortalEverthing.Models.TagGame;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class AnimeCatalogController : Controller
    {
        private IAnimeCatalogRepository _animeCatalogRepository;

        private TagGameBuilder _tagGameBuilder;

        public AnimeCatalogController(IAnimeCatalogRepository animeCatalogRepository, TagGameBuilder tagGameBuilder)
        {
            _animeCatalogRepository = animeCatalogRepository;
            _tagGameBuilder = tagGameBuilder;
        }

        public IActionResult Index(int? count)
        {
            if (!_animeCatalogRepository.Any())
            {
                CreateDefoltAnimeCatalog(count);
            }

            var animesFromDb = _animeCatalogRepository.GetAll();

            var animesViewModels = animesFromDb
                .Select(dbAnime =>
                    new AnimeCatalogViewModel
                    {
                        Id = dbAnime.Id,
                        Name = dbAnime.Name,
                        ImageSrc = dbAnime.ImageSrc,
                    }
                )
                .ToList();

            return View(animesViewModels);
        }

        private void CreateDefoltAnimeCatalog(int? count)
        {
            for (int i = 0; i < (count ?? 4); i++)
            {
                var animeNumber = (i % 4) + 1;
                var dataModel = new AnimeCatalogData
                {
                    Name = $"Anime {animeNumber}",
                    ImageSrc = $"/images/AnimeCatalog/Anime{animeNumber}.jpg",
                };

                _animeCatalogRepository.Add(dataModel);
            }
        }

        public IActionResult TagGame()
        {

            _tagGameBuilder.Create();

            var builder = _tagGameBuilder.GetBuilder();

            var tagsLength = builder.GetField().GetTags().GetLength(0);

            var viewModel = new TagGameViewModel
            {
                Tags = builder.GetField().GetStringTags()
            };

            return View(viewModel);
        }

        public IActionResult MoveTile(int x, int y)
        {
            var builder = _tagGameBuilder.GetBuilder();
            var field = builder.GetField();
            field.ChangePositions(x, y);

            var viewModel = new TagGameViewModel
            {
                Tags = field.GetStringTags()
            };

            return View("TagGame", viewModel);
        }
    }
}
