namespace WebPortalEverthing.Services
{
    public class FileProvider
    {
        private IWebHostEnvironment _webHostEnvironment;

        public FileProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetNewFileName(string oldFileName)
        {
            var newFileName = Guid.NewGuid().ToString();

            if (!oldFileName.Contains('.'))
            {
                return newFileName;
            }

            var extension = Path.GetExtension(oldFileName);
            newFileName = newFileName + extension;

            return newFileName;
        }

        public bool Save(IFormFile formFile, string fileName)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var folderPath = Path.Combine(webRootPath, "files");

            if (!Path.Exists(folderPath))
            { 
                return false;
            }

            var path = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(path, FileMode.Create);
            formFile
                .CopyToAsync(fileStream)
                .Wait();

            return true;
        }
    }
}
