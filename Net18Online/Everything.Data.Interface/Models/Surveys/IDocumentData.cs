namespace Everything.Data.Interface.Models.Surveys
{
    public interface IDocumentData : IBaseModel
    {
        string Title { get; set; }
        string OriginalFileName { get; set; }
        string StorageFileName { get; set; }
        string ContentType { get; set; }
        long Length { get; set; }
    }
}
