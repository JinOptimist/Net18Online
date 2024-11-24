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
            newFileName += extension;

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

            var filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            formFile
                .CopyToAsync(fileStream)
                .Wait();

            return true;
        }

        public bool Delete(string fileName)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(webRootPath, "files", fileName);

            if (!File.Exists(filePath))
            {
                return false;
            }

            File.Delete(filePath);

            return true;
        }
    }
}
