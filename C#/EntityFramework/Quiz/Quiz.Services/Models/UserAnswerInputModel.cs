using System.Collections.Generic;

namespace Quiz.Services.Models
{
    public class UserAnswerInputModel
    {
        public string Userid { get; set; }

        public ICollection<AnswerInputModel> Answers { get; set; }
    }
}