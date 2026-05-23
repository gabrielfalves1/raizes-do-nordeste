using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;

namespace raizes_do_nordeste.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();

            builder.HasIndex(t => t.Token).IsUnique();

            builder.Property(t => t.Token)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.DataCriacao)
                .IsRequired();

            builder.Property(t => t.DataExpiracao)
                .IsRequired();

            builder.Property(t => t.Revogado)
                .IsRequired();

            builder.Property(t => t.EnderecoIP)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.HasOne(t => t.Usuario)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}