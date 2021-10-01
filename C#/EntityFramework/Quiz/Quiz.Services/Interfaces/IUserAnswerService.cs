using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Services.Interfaces
{
    public interface IUserAnswerService
    {
        void Add(string userId, int questionId, int answerId);
    }
}
