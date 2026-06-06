using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;

namespace raizes_do_nordeste.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(p => p.CanalPedido)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.ValorTotal)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.DataCriacao)
                .IsRequired();

            builder.Property(p => p.DataAtualizacao)
                .IsRequired();

            builder.HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Unidade)
                .WithMany()
                .HasForeignKey(p => p.UnidadeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
