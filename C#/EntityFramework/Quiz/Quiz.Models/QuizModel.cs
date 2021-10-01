using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Models
{
    public class QuizModel
    {
        public QuizModel()
        {
            this.Questions = new HashSet<Question>();
            this.UserQuizzes = new HashSet<UserQuiz>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
