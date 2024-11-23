using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Models.Surveys
{
    public class DocumentData : BaseModel, IDocumentData
    {
        public string Title { get; set; }
        public string OriginalFileName { get; set; }
        public string StorageFileName { get; set; }
        public string ContentType { get; set; }
        public long Length { get; set; }
    }
}