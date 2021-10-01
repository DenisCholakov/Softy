using System;
using System.Collections.Generic;
using System.Text;
using Quiz.Data;
using Quiz.Models;
using Quiz.Services.Interfaces;

namespace Quiz.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext _context;

        public AnswerService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public int Add(string title, int points, bool isCorrect, int questionId)
        {
            var answer = new Answer
            {
                Title = title,
                Points = points,
                IsCorrect = isCorrect,
                QuestionId = questionId
            };

            this._context.Answers.Add(answer);
            this._context.SaveChanges();

            return answer.Id;
        }
    }
}
