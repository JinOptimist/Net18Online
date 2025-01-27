using Everything.Data.Interface.Enums;

namespace Everything.Data.Interface.Models.Surveys
{
    public interface IQuestionData : IBaseModel
    {
        string Title { get; set; }
        bool IsRequired { get; set; }
        AnswerType AnswerType { get; set; }
    }
}
