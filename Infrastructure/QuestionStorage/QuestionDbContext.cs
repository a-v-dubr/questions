using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    /// <summary>
    /// Initializes a new instance of QuestionDbContext class
    /// </summary>
    internal class QuestionDbContext : DbContext
    {
        private readonly static string _connectionString = $"Data Source={QuestionRepository.DbName}";

        internal DbSet<Question> Questions { get; set; }
        internal DbSet<Answer> Answers { get; set; }
        internal DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
