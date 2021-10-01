using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Services.Interfaces
{
    public interface IAnswerService
    {
        int Add(string title, int points, bool isCorrect, int questionId);
    }
}
