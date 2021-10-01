
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Quiz.Services.Interfaces;

namespace Quiz.Services
{
    public class UserQuizService : IUserQuizService
    {
        private readonly ApplicationDbContext _context;

        public UserQuizService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(string userId, int quizId)
        {
            var userQuiz = new UserQuiz
            {
                IdentityUserId = userId,
                QuizId = quizId
            };

            this._context.UsersQuizzes.Add(userQuiz);
            this._context.SaveChanges();
        }

        public int UserResult(string userId, int quizId)
        {
            var quiz = this._context.Quizzes
                .Include(quiz => quiz.Questions)
                .FirstOrDefault(q => q.Id == quizId);
                

            var userAnswers = this._context.UserAnswers
                .Include(ua => ua.Answer)
                .Where(ua => quiz.Questions.Contains(ua.Question));

            var points = 0;

            foreach (var userAnswer in userAnswers)
            {
                if (userAnswer.Answer.IsCorrect)
                {
                    points += userAnswer.Answer.Points;
                }
            }

            return points;
        }
    }
}
