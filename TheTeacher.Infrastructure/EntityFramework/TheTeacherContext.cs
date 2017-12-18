using Microsoft.EntityFrameworkCore;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.EntityFramework
{
    public class TheTeacherContext : DbContext
    {
        private readonly SqlSettings _sqlSettings;
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        
        
        public TheTeacherContext(DbContextOptions<TheTeacherContext> options, SqlSettings sqlSettings ) : base(options)
        {
            _sqlSettings = sqlSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(_sqlSettings.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_sqlSettings.DbName);

                return;
            }
            optionsBuilder.UseSqlServer(_sqlSettings.ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userBuilder = modelBuilder.Entity<User>();          

            userBuilder.HasKey(x => x.Id);
            var teacherBuilder = modelBuilder.Entity<Teacher>();
            teacherBuilder.HasKey(x => x.UserID);
        }

    }
}