using Microsoft.EntityFrameworkCore;

#nullable disable

namespace NetShop
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consumer> Consumers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Clothes> Clothes { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Paveltech\\source\\repos\\NetShop\\NetShop\\Data\\NetShopDB.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Consumer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Adress).HasMaxLength(150);

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FIO");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(12);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Clothes>(entity =>
            {
                entity.Property(e => e.ClothesId).ValueGeneratedNever();
                entity.Property(e => e.ItemId)
                .IsRequired();

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.DateBill)
                    .IsRequired();

                entity.Property(e => e.Orders)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.WhoOrdered)
                    .IsRequired();
                entity.Property(e => e.IsDelivery)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}