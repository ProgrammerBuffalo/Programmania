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

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ProgrammaniaDBContext(DbContextOptions<ProgrammaniaDBContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Document>(d => { d.HasNoKey(); d.ToView("DocumentsView"); });
            base.OnModelCreating(builder);
        }
    }
}
