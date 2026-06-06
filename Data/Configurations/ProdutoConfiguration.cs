using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;
using System;

namespace raizes_do_nordeste.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(p => p.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(p => p.Preco)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.Ativo)
                .IsRequired();

            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            var categoriaIdFixa = Guid.Parse("55555555-5555-5555-5555-555555555555");

            builder.HasData(
                new Produto
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Nome = "Cuscuz Completo com Charque",
                    Descricao = "Cuscuz de milho no capricho com charque desfiada e queijo coalho.",
                    Preco = 25.90m,
                    Ativo = true,
                    CategoriaId = categoriaIdFixa
                },
                new Produto
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Nome = "Tapioca de Carne de Sol",
                    Descricao = "Tapioca rendada na manteiga de garrafa com carne de sol e nata.",
                    Preco = 19.50m,
                    Ativo = true,
                    CategoriaId = categoriaIdFixa
                }
            );
        }
    }
}