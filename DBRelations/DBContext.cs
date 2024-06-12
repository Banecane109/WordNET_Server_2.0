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
                .HasKey(w => w.Id);

            modelBuilder.Entity<AssociatedWord>()
                .HasKey(aw => aw.Id);

            modelBuilder.Entity<Statistics>()
                .HasKey(s => s.Id);


            modelBuilder.Entity<AssociatedWord>()
                .HasOne<Word>()
                .WithMany(w => w.AssociatedWords)
                .HasForeignKey(aw => aw.WordId);

            modelBuilder.Entity<Statistics>()
                .HasOne<AssociatedWord>()
                .WithOne()
                .HasForeignKey<Statistics>(s => s.AssociatedWordId);
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
