using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Data;
using Quiz.Services;
using Quiz.Services.Interfaces;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var quizService = serviceProvider.GetService<IQuizService>();
            var quiz = quizService.GqtQuizById(1);

            Console.WriteLine(quiz.Title);

            foreach (var question in quiz.Questions)
            {
                Console.WriteLine($"  {question.Title}");

                foreach (var answer in question.Answers)
                {
                    Console.WriteLine($"    {answer.Title}");
                }
            }

            //var questionService = serviceProvider.GetService<IQuestionService>();
            //questionService.Add("What is Entity Framework Core", 1);

            //var answerService = serviceProvider.GetService<IAnswerService>();
            //answerService.Add("It is an MicroORM.", 0, false, 1);

            //var userAnswerService = serviceProvider.GetService<IUserAnswerService>();
            //userAnswerService.Add("193e87ac-f855-470c-849e-fe84b3e1ea56", 1, 1);

            var userQuizService = serviceProvider.GetService<IUserQuizService>();
            var points = userQuizService.UserResult("193e87ac-f855-470c-849e-fe84b3e1ea56", 1);

            Console.WriteLine(points);

            //userQuizService.Add("193e87ac-f855-470c-849e-fe84b3e1ea56", 1);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IQuizService, QuizService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IUserQuizService, UserQuizService>();
            services.AddTransient<IUserAnswerService, UserAnswerService>();
        }
    }
}
