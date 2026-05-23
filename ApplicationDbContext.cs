using Microsoft.EntityFrameworkCore;
using raizes_do_nordeste.Entities;

namespace raizes_do_nordeste
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(x => x.ToTable("Usuarios"));

            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>().Property(u => u.Nome)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Usuario>().Property(u => u.Email)
                .HasMaxLength(150)
                .IsRequired();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Usuario>().Property(u => u.SenhaHash)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<Usuario>().Property(u => u.Telefone)
                .HasMaxLength(20)
                .IsRequired(false);

            modelBuilder.Entity<Usuario>().Property(u => u.DataCadastro)
                .IsRequired();

            modelBuilder.Entity<Usuario>().Property(u => u.Perfil)
                .IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Status)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>(x => x.ToTable("RefreshTokens"));

            modelBuilder.Entity<RefreshToken>().HasKey(t => t.Id);

            modelBuilder.Entity<RefreshToken>().HasIndex(t => t.Token).IsUnique();
            modelBuilder.Entity<RefreshToken>().Property(t => t.Token)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>().Property(t => t.DataCriacao)
                .IsRequired();
            modelBuilder.Entity<RefreshToken>().Property(t => t.DataExpiracao)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>().Property(t => t.Revogado)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>().Property(t => t.EnderecoIP)
                .HasMaxLength(50)
                .IsRequired(false);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
