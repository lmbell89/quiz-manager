using Microsoft.EntityFrameworkCore;
using QuizManager.Data.Entities;

namespace QuizManager.Data
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answer { get; set; }
    }
}
