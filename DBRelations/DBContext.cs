using Microsoft.EntityFrameworkCore;
using WordNET_Server_2._0.Models;

namespace WordNET_Server_2._0.DBRelations
{
    public class DBContext : DbContext
    {
        public DbSet<Word> Word { get; set; }
        public DbSet<AssociatedWord> AssociatedWord { get; set; }
        public DbSet<Statistics> Statistics { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasMany(w => w.AssociatedWords)
                .WithOne(aw => aw.Word)
                .HasForeignKey(aw => aw.WordId);

            modelBuilder.Entity<AssociatedWord>()
                .HasOne(aw => aw.Statistics)
                .WithOne(s => s.AssociatedWord)
                .HasForeignKey<Statistics>(s => s.AssociatedWordId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(local)\SQLEXPRESS;Database=WordNETDB;Trusted_Connection=True;TrustServerCertificate=True;",
                    options =>
                    {
                        options.EnableRetryOnFailure();
                    });

                optionsBuilder.EnableSensitiveDataLogging();
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
