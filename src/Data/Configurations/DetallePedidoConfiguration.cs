using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<Detalle_Pedido>
    {
        public void Configure(EntityTypeBuilder<Detalle_Pedido> builder)
        {
            builder.ToTable("DetallePedidos");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(d => d.Cantidad)
                .IsRequired();

            builder.Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(d => d.Pedido)
                .WithMany()
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Menu)
                .WithMany()
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
