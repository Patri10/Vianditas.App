using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido>
    {
        public void Configure(EntityTypeBuilder<DetallePedido> builder)
        {
            builder.ToTable("DetallePedidos");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(d => d.Cantidad)
                .IsRequired();

            builder.Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(d => d.Pedido)
                .WithMany(p => p.DetallePedido)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Menu)
                .WithMany()
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
