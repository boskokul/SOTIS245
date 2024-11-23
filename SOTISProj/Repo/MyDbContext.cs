using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace SOTISProj.Repo
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //For value object pair class
            modelBuilder.Entity<ConnectAnswer>()
            .Property(q => q.ConnectedPairs)
             .HasConversion(
                 v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),  
            v => JsonSerializer.Deserialize<List<Pair>>(v, new JsonSerializerOptions { WriteIndented = false }) 
             );

            // Configure BellongingTerms to be stored as JSON
            modelBuilder.Entity<ConnectQuestion>()
                .Property(q => q.BellongingTerms)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),
                    v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions { WriteIndented = false })
                );
            modelBuilder.Entity<ConnectQuestion>()
            .Property(q => q.BelongTerms)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),
                v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions { WriteIndented = false })
            );
        }

        public DbSet<Field> Fields { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins{ get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<DefinitionQuestion> DefinitionQuestions { get; set; }
        public DbSet<DefinitionAnswer> DefiniftionAnswers { get; set; }
        public DbSet<ConnectQuestion> ConnectQuestions { get; set; }
        public DbSet<ConnectAnswer> ConnectAnswers { get; set; }
        public DbSet<TestSample> TestSamples { get; set; }
    }
}

