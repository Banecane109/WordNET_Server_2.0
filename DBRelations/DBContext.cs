using Microsoft.EntityFrameworkCore;
using WordNET_Server_2._0.Models;

namespace WordNET_Server_2._0.DBRelations
{
    public class DBContext : DbContext
    {
        public DbSet<Word> Word { get; set; }
        public DbSet<AssociatedWord> AssociatedWord { get; set; }
        public DbSet<Questionee> Questionee { get; set; }
        public DbSet<AssociatedWordQuestionee> AssociatedWordQuestionees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssociatedWordQuestionee>()
                .HasKey(aws => new
                {
                    aws.AssociatedWordId,
                    aws.QuestioneeId,
                });

            modelBuilder.Entity<AssociatedWordQuestionee>()
                .HasOne(aws => aws.AssociatedWord)
                .WithMany(aw => aw.AssociatedWordQuestionees)
                .HasForeignKey(aws => aws.AssociatedWordId);

            modelBuilder.Entity<AssociatedWordQuestionee>()
                .HasOne(aws => aws.Questionee)
                .WithMany(q => q.AssociatedWordQuestionees)
                .HasForeignKey(aws => aws.QuestioneeId);

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
