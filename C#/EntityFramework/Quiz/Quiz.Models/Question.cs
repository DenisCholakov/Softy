using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [ForeignKey(nameof(QuizModel))]
        public int QuizId { get; set; }

        public QuizModel QuizModel { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }

    }
}