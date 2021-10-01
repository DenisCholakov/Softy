using System;
using System.Collections.Generic;
using System.Text;
using Quiz.Data;
using Quiz.Models;
using Quiz.Services.Interfaces;
using Quiz.Services.Models;

namespace Quiz.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly ApplicationDbContext _context;

        public UserAnswerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(string userId, int questionId, int answerId)
        {
            var userAnswer = new UserAnswer
            {
                IdentityUserId = userId,
                QuestionId = questionId,
                AnswerId = answerId
            };

            this._context.UserAnswers.Add(userAnswer);
            this._context.SaveChanges();
        }

        public void BulkAddUserAnswer(UserAnswerInputModel userAnswersInput)
        {
            var usersAnswers = new List<UserAnswer>();

            foreach (var answer in userAnswersInput.Answers)
            {
                var userAnswer = new UserAnswer
                {
                    IdentityUserId = userAnswersInput.Userid,
                    QuestionId = answer.QuestionId,
                    AnswerId = answer.AnswerId
                };

                usersAnswers.Add(userAnswer);
            }

            this._context.AddRange(usersAnswers);
            this._context.SaveChanges();
        }
    }
}
