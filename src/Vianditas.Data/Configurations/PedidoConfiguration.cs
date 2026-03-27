using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.Fecha)
                .IsRequired();

            builder.Property(p => p.Total)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Estado)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.DetallePedido)
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Pago)
                .WithOne(pg => pg.Pedido)
                .HasForeignKey<Pago>(pg => pg.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
