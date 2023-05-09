using Microsoft.EntityFrameworkCore;
using Player.Infraestructure.Model;

namespace Player.Infraestructure.Contexts
{
    public partial class PlayersContext : DbContext
    {
        public PlayersContext()
        {
        }

        public PlayersContext(DbContextOptions<PlayersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlayerModel> Players { get; set; } = null!;
        public virtual DbSet<PlayerFunction> PlayerFunctions { get; set; } = null!;
        public virtual DbSet<PlayerStat> PlayerStats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=aws-ludus.cdbvnpu4ioji.sa-east-1.rds.amazonaws.com; Database=Players; User Id=admin; Password=root1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerModel>(entity =>
            {
                entity.ToTable("Player", "Ludus");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CPF");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FkFunctionId).HasColumnName("FK_Function_Id");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RepresentativeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RepresentativePhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<PlayerFunction>(entity =>
            {
                entity.ToTable("PlayerFunction", "Ludus");

                entity.Property(e => e.AcronymFunction)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NameFunction)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlayerStat>(entity =>
            {
                entity.ToTable("PlayerStats", "Ludus");

                entity.Property(e => e.FkPlayerId).HasColumnName("FK_Player_Id");

                entity.HasOne(d => d.FkPlayer)
                    .WithMany(p => p.PlayerStats)
                    .HasForeignKey(d => d.FkPlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Player_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
