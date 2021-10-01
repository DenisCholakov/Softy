using System;
using System.Collections.Generic;
using System.Text;
using Quiz.Data;
using Quiz.Models;
using Quiz.Services.Interfaces;

namespace Quiz.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public int Add(string Title, int quizId)
        {
            var question = new Question
            {
                Title = Title,
                QuizId = quizId
            };

            this._context.Questions.Add(question);
            this._context.SaveChanges();

            return question.Id;
        }
    }
}
