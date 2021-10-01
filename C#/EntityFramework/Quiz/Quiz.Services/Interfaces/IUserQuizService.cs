namespace Quiz.Services.Interfaces
{
    public interface IUserQuizService
    {
        void Add(string userId, int quizId);

        int UserResult(string userId, int quizId);
    }
}
