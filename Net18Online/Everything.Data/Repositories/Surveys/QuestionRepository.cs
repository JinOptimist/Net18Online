﻿using Everything.Data.Interface.Enums;
using Everything.Data.Interface.Models.Surveys;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models.SqlRawModels;
using Everything.Data.Models.Surveys;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories.Surveys
{
    public interface IQuestionRepositoryReal : IQuestionRepository<QuestionData>
    {
        IEnumerable<QuestionToAnswerModel> GetQuestionsForSurveyByTaking(int takingId);
    }

    public class QuestionRepository : BaseRepository<QuestionData>, IQuestionRepositoryReal
    {
        public QuestionRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public override int Add(QuestionData data)
        {
            throw new NotImplementedException($"Use method {nameof(Create)} to create a new Question");
        }

        public int Create(int surveyId)
        {
            var survey = _webDbContext.Surveys.First(x => x.Id == surveyId);

            var data = new QuestionData()
            {
                Title = string.Empty,
                IsRequired = false,
                AnswerType = AnswerType.TextString,
                Survey = survey
            };

            _dbSet.Add(data);
            _webDbContext.SaveChanges();
            
            return data.Id;
        }

        public void UpdateTitle(int id, string value)
        {
            var question = Get(id);

            question.Title = value;

            _webDbContext.SaveChanges();
        }

        public void UpdateRequired(int id, bool value)
        {
            var question = Get(id);

            question.IsRequired = value;

            _webDbContext.SaveChanges();
        }

        public void UpdateAnswerType(int id, AnswerType value)
        {
            var question = Get(id);

            question.AnswerType = value;

            _webDbContext.SaveChanges();
        }

        public IEnumerable<IQuestionData> GetQuestionsForSurvey(int surveyId)
        {
            return _dbSet
                .Where(i => i.Survey.Id == surveyId)
                .ToList();
        }

        public IEnumerable<QuestionToAnswerModel> GetQuestionsForSurveyByTaking(int takingId)
        {

            var sql = @$"
SELECT	Answer.id AS AnswerId,
		Questions.Title,
		Questions.IsRequired,
		Questions.AnswerType,
		Answer.[Text] AS AnswerText
FROM dbo.TakingUserSurveys TakingSurveys
INNER JOIN dbo.AnswerToQuestions Answer ON TakingSurveys.id = Answer.TakingUserSurveyId
INNER JOIN dbo.Questions Questions ON Answer.QuestionId = Questions.Id
WHERE TakingSurveys.Id = {takingId}";

            return _webDbContext
                .Database
                .SqlQueryRaw<QuestionToAnswerModel>(sql)
                .ToList();
        }
    }
}
