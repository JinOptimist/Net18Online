using System.ComponentModel;

namespace WebPortalEverthing.Models.Surveys
{
    public class DocumentCreateOrEditViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Title { get; set; }
        [DisplayName("Файл")]
        public IFormFile FormFile {  get; set; }
    }
}
