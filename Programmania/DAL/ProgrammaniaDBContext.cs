using Microsoft.EntityFrameworkCore;
using Programmania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.DAL
{
    public class ProgrammaniaDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<UserDiscipline> UserDisciplines { get; set; } 
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserChallenge> UserChallenges { get; set; }
        public DbSet<Challenge> Challenges { get; set; }

        public ProgrammaniaDBContext(DbContextOptions<ProgrammaniaDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Document>(d => { d.HasNoKey(); d.ToView("DocumentsView"); });
            builder.Entity<UserDiscipline>().HasKey(table => new {
                table.UserId,
                table.DisciplineId
            });

            builder.Entity<UserChallenge>().HasKey(table => new
            {
                table.UserId,
                table.ChallengeId
            });

            builder.Entity<Test>(entity => entity.HasCheckConstraint("CK_Tests_Correct", "[Correct] > 0 AND [Correct] < 5"));
            base.OnModelCreating(builder);
        }
    }
}
