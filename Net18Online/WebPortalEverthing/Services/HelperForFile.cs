using Everything.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace WebPortalEverthing.Services
{
    public class HelperForFile
    {
        private IWebHostEnvironment _webHostEnvironment;
        private AuthService _authService;
        private IUserRepositryReal _userRepositry;

        public HelperForFile(IWebHostEnvironment webHostEnvironment, AuthService authService, IUserRepositryReal userRepositry)
        {
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
            _userRepositry = userRepositry;
        }

        public string CreateImageFileForCake(IFormFile image)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;

            var userName = _authService.GetName();
            var userId = _authService.GetUserId()!.Value;
            var countCreationImg = _userRepositry.GetNewIdForImage(userId);
            var imageFileName = $"{userName}Cake-{countCreationImg}.jpg";

            var path = Path.Combine(webRootPath, "images", "avatars", imageFileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                image
                    .CopyToAsync(fileStream)
                    .Wait();
            }

            var pathImg = $"/images/avatars/{imageFileName}";

            return pathImg;
        }

        public void DeleteImageFileForCake(string pathImg)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;

            var path = Path.Combine(webRootPath, pathImg.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                return;
            }
        }
    }
}
