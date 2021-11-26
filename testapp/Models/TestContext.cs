using Microsoft.EntityFrameworkCore;

namespace testapp.Models
{
    public class TestContext : DbContext
    {
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<ProfessorsModel> Professors { get; set; }
        public DbSet<ProgramsModel> Programs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupForeignKey);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=D:\Work\ittreands\test-back\sqlite\test2.db"); 
    }
}