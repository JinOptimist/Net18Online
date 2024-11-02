namespace Everything.Data.Interface.Models.Surveys
{
    public interface IStatusData : IBaseModel
    {
        string Title { get; set; }
        string ImagesSrc { get; set; }
    }
}
