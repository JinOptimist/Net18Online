namespace Everything.Data.Interface.Models.Surveys
{
    public interface IStatus
    {
        int Id { get; set; }
        string Title { get; set; }
        string ImagesSrc { get; set; }
    }
}
