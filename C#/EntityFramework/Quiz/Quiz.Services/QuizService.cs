using System.Linq;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Quiz.Services.Interfaces;
using Quiz.Services.Models;

namespace Quiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext _context;

        public QuizService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Add(string title)
        {
            var quiz = new QuizModel
            {
                Title = title
            };

            this._context.Quizzes.Add(quiz);
            this._context.SaveChanges();
        }

        public QuizViewModel GqtQuizById(int quizId)
        {
            var quiz = this._context.Quizzes
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefault(x => x.Id == quizId);

            var quizViewModel = new QuizViewModel
            {
                Title = quiz.Title,
                Questions = quiz.Questions.Select(question => new QuestionViewModel
                {
                    Id = question.Id,
                    Title = question.Title,
                    Answers = question.Answers.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        Title = a.Title,
                    })
                })
            };

            return quizViewModel;
        }
    }
}
