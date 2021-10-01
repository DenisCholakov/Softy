using Quiz.Services.Models;

namespace Quiz.Services.Interfaces
{
    public interface IQuizService
    {
        void Add(string title);

        QuizViewModel GqtQuizById(int quizId);
    }
}
