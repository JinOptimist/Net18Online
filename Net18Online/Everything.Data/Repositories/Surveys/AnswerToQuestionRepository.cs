﻿using Everything.Data.Interface.Enums;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories.Surveys
{
    public interface IAnswerToQuestionRepositoryReal : IAnswerToQuestionRepository<AnswerToQuestionData>
    {
    }

    public class AnswerToQuestionRepository : BaseRepository<AnswerToQuestionData>, IAnswerToQuestionRepositoryReal
    {
        public AnswerToQuestionRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void SetTextValue(int id, string? value)
        {
            var answer = Get(id);
            answer.Text = value;

            _webDbContext.SaveChanges();
        }

        public bool Any(int userId, int takingId, int questionId)
        {
            return _dbSet
                .Where(x => x.TakingUserSurvey.Id == takingId 
                    && x.Question.Id == questionId 
                    && x.TakingUserSurvey.User.Id == userId)
                .Any();
        }

        /// <summary>
        /// Returns answer IDs, not question IDs!
        /// </summary>
        public List<int> GetIdsUnansweredQuestions(int takingId)
        {
            var answerTypes = new List<AnswerType>()
            { 
                AnswerType.TextString,
                AnswerType.TextParagraph
            };

            return _dbSet
                .Where(x => x.TakingUserSurvey.Id == takingId
                    &&  (       (answerTypes.Contains(x.Question.AnswerType) && string.IsNullOrEmpty(x.Text))
                            ||  (!answerTypes.Contains(x.Question.AnswerType))
                        )
                    )
                .Select(x => x.Id)
                .ToList();
        }
    }
}