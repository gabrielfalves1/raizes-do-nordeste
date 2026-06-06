using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;

namespace raizes_do_nordeste.Data.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("ItensPedido");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(i => i.Quantidade)
                .IsRequired();

            builder.Property(i => i.PrecoUnitario)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(i => i.Subtotal)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Produto)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
