using Microsoft.EntityFrameworkCore;
namespace SOTISProj.Repo
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<DefinitionQuestion> DefinitionQuestions { get; set; }
        public DbSet<DefinitionAnswer> DefiniftionAnswers { get; set; }
        public DbSet<ConnectQuestion> ConnectQuestions { get; set; }
        public DbSet<ConnectAnswer> ConnectAnswers { get; set; }
    }
}

