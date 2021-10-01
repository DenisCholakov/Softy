using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Quiz.Models
{
    public class UserAnswer
    {
        public string IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public int AnswerId { get; set; }

        public Answer Answer { get; set; }
    }
}
