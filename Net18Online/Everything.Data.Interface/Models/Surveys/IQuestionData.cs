namespace Everything.Data.Interface.Models.Surveys
{
    public interface IQuestionData : IBaseModel
    {
        string Title { get; set; }
        bool IsRequired { get; set; }
    }
}
