using System;
using Labs.Feedback.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Labs.Feedback.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mensagem> Mensagens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "dbfeedback");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mensagem>()
                .HasKey(m => m.Ident);

            modelBuilder.Entity<Mensagem>()
                .Property(m => m.Descricao)
                .HasMaxLength(100);

            modelBuilder.Entity<Mensagem>()
                .Property(m => m.Categoria)
                .HasConversion<string>();
        }
    }
}
