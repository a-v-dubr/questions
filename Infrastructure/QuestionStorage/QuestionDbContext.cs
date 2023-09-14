using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    /// <summary>
    /// Initializes a new instance of QuestionDbContext class
    /// </summary>
    internal class QuestionDbContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbHelper.ConnectionString);
        }
    }
}
