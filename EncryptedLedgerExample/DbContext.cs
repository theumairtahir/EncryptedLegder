using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;

namespace EncryptedLedgerExample
{
    public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly string connectionString;

        public DbContext()
        {
            connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\New folder\\Visual Studio 2019\\projects\\EncryptedLegder\\EncryptedLedgerExample\\Database.mdf\";Integrated Security=True";
        }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
            try
            {
                connectionString = options.GetExtension<SqlServerOptionsExtension>().ConnectionString;
            }
            catch (Exception)
            {
                connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\New folder\\Visual Studio 2019\\projects\\EncryptedLegder\\EncryptedLedgerExample\\Database.mdf\";Integrated Security=True";
            }
            OnCreated();
        }
        public DbContext(string connectionString) : base(GetOptions(connectionString))
        {
            this.connectionString = connectionString;
            OnCreated();
        }
        public virtual DbSet<Table> Table { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>(entity =>
            {
                entity.Property(e => e.Balance)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Credit)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Debit)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Tag1).IsUnicode(false);

                entity.Property(e => e.Tag2).IsUnicode(false);

                entity.Property(e => e.Tag3).IsUnicode(false);

                entity.Property(e => e.TransactionDateTime)
                    .IsRequired()
                    .IsUnicode(false);
                entity.Property(e => e.Signature)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        partial void OnCreated();
    }
}
