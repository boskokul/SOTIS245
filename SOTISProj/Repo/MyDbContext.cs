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
    }
}

