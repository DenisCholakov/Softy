using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Quiz.Models
{
    public class UserQuiz
    {
        public string IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public int QuizId { get; set; }

        public QuizModel Quiz { get; set; }
    }
}
