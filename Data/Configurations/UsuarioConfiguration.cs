using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;

namespace raizes_do_nordeste.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(u => u.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.SenhaHash)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(u => u.Telefone)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(u => u.DataCadastro)
                .IsRequired();

            builder.Property(u => u.Perfil)
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();
        }
    }
}