using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using raizes_do_nordeste.Entities;

namespace raizes_do_nordeste.Data.Configurations
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamentos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasValueGenerator<SequentialGuidValueGenerator>();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.Metodo)
                .IsRequired();

            builder.Property(p => p.Gateway)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.TransactionId)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(p => p.DataPagamento)
                .IsRequired(false);

            builder.Property(p => p.ValorPago)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(p => p.Pedido)
                .WithOne(pe => pe.Pagamento)
                .HasForeignKey<Pagamento>(p => p.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
