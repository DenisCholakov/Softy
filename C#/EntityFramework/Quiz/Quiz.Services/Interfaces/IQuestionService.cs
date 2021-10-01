using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Services.Interfaces
{
    public interface IQuestionService
    {
        int Add(string Title, int quizId);
    }
}
