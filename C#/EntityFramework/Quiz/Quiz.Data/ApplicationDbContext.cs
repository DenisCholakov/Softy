using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz.Models;

namespace Quiz.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<QuizModel> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }

        public DbSet<UserQuiz> UsersQuizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAnswer>()
                .HasKey(x => new {x.IdentityUserId, x.QuestionId});

            builder.Entity<UserAnswer>()
                .HasOne(x => x.Question)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserQuiz>()
                .HasKey(x => new {x.IdentityUserId, x.QuizId});

            base.OnModelCreating(builder);
        }
    }
}
