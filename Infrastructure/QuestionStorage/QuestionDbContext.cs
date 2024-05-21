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

        internal DbSet<Question> Questions { get; private set; }
        internal DbSet<Answer> Answers { get; private set; }
        internal DbSet<Category> Categories { get; private set; }

        public QuestionDbContext()
        {
            try
            {
                Database.Migrate();
            }
            catch (Exception)
            {
                Database.EnsureDeleted();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().Property(p => p.RepeateInPeriod).HasConversion<int>().HasDefaultValue(Question.Repetitions.EnableAsCreated);
        }
    }
}
