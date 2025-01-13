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
    public class ApiGirlController : ControllerBase
    {
        private IAnimeGirlRepositoryReal _animeGirlRepository;
        private AuthService _authService;

        public ApiGirlController(IAnimeGirlRepositoryReal animeGirlRepository, AuthService authService)
        {
            _animeGirlRepository = animeGirlRepository;
            _authService = authService;
        }

        public List<GirlViewModel> GetGirls()
        {
            var girlsFromDb = _animeGirlRepository.GetAllWithCreatorsAndManga();
            var girlsViewModels = girlsFromDb
                .Select(dbGirl =>
                    new GirlViewModel
                    {
                        Id = dbGirl.Id,
                        Name = dbGirl.Name,
                        ImageSrc = dbGirl.ImageSrc,
                        Tags = new List<string>(),
                        CreatorName = dbGirl.Creator?.Login ?? "Неизвестный",
                        MangaName = dbGirl.Manga?.Title ?? "Из фанфика",
                        CanDelete = false,
                        LikeCount = dbGirl.UsersWhoLikeIt.Count(),
                        IsLiked = false
                    }
                )
                .ToList();
            return girlsViewModels;
        }

        public bool UpdateName(string newName, int id)
        {
            Thread.Sleep(5 * 1000);

            if (newName.Contains("boris"))
            {
                return false;
            }

            _animeGirlRepository.UpdateName(id, newName);
            return true;
        }

        public bool Remove(int id)
        {
            _animeGirlRepository.Delete(id);
            return true;
        }

        //public int Create(string name, string url, int mangaId)
        public GirlViewModel Create(ApiGirlCreationViewModel data)
        {
            var currentUserId = _authService.GetUserId();

            var dataGirl = new GirlData
            {
                Name = data.Name,
                ImageSrc = data.Url,
            };

            var id = _animeGirlRepository
                .Create(dataGirl, currentUserId!.Value, data.MangaId.Value);
            var dbGirl = _animeGirlRepository.GetWithCreatorsAndManga(id);

            var viewModel = new GirlViewModel
            {
                Id = dbGirl.Id,
                Name = dbGirl.Name,
                ImageSrc = dbGirl.ImageSrc,
                Tags = new List<string>(),
                CreatorName = dbGirl.Creator?.Login ?? "Неизвестный",
                MangaName = dbGirl.Manga?.Title ?? "Из фанфика",
                CanDelete = dbGirl.Creator is null
                            || dbGirl.Creator?.Id == currentUserId
            };

            return viewModel;
        }

        public void CreateGirlForGuess(ApiGirlCreationViewModel girlDto)
        {
            var girl = new GirlData
            {
                Name = girlDto.Name,
                ImageSrc = girlDto.Url
            };


            _animeGirlRepository.Add(girl);
        }

        [Authorize]
        public bool Like(int girlId)
        {
            var userId = _authService.GetUserId()!.Value;

            return _animeGirlRepository.LikeGirl(girlId, userId);
        }
    }
}
