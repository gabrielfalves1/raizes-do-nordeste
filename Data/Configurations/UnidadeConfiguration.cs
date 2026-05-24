using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;

namespace raizes_do_nordeste.Data.Configurations
{
    public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.ToTable("Unidades");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
               .HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(u => u.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Endereco)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Cidade)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Estado)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Telefone)
                .HasMaxLength(20);

            builder.Property(u => u.Status)
                .IsRequired();

        }
    }
}
