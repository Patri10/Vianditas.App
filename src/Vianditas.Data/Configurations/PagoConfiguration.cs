using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class PagoConfiguration : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            builder.ToTable("Pagos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.MercadoPagoId)
                .HasMaxLength(100);

            builder.Property(p => p.LinkdePago)
                .HasMaxLength(500);

            builder.Property(p => p.Estado)
                .HasMaxLength(50)
                .HasDefaultValue("Pendiente");

            builder.Property(p => p.FechaCreacion)
                .IsRequired();

            builder.HasOne(p => p.Pedido)
                .WithOne(pd => pd.Pago)
                .HasForeignKey<Pago>(p => p.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
