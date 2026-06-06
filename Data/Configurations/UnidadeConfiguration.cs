using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;
using raizes_do_nordeste.Enums; // Assumindo que StatusEnum está aqui
using System;

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

            builder.Property(u => u.Nome).HasMaxLength(150).IsRequired();
            builder.Property(u => u.Endereco).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Estado).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Telefone).HasMaxLength(20);
            builder.Property(u => u.Status).IsRequired();

            builder.HasData(
                new Unidade
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Nome = "Raízes do Nordeste - Matriz Boa Viagem",
                    Endereco = "Av. Boa Viagem, 123",
                    Cidade = "Recife",
                    Estado = "PE",
                    Telefone = "81999990000",
                    Status = StatusEnum.Ativo
                },
                new Unidade
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Nome = "Raízes do Nordeste - Shopping Riomar",
                    Endereco = "Av. República do Líbano, 251",
                    Cidade = "Recife",
                    Estado = "PE",
                    Telefone = "81999991111",
                    Status = StatusEnum.Ativo
                }
            );
        }
    }
}