using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;
using System;

namespace raizes_do_nordeste.Data.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(c => c.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasData(
                new Categoria
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Nome = "Pratos Regionais",
                    Descricao = "Comidas típicas nordestinas servidas a qualquer hora do dia."
                }
            );
        }
    }
}