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
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.Estado)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.UsuarioId)
                .IsRequired();

            builder.Property(p => p.CategoriaId)
                .IsRequired();

            builder.Property(p => p.Detalles)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(p => p.Total)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            
            builder.Property(p => p.HoraCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd()
                .IsRequired();


            builder.HasOne<Usuarios>()
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Categoria>()
                .WithMany()
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<Detalle_Pedido>()
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Pago>()
                .WithOne(p => p.Pedido)
                .HasForeignKey<Pago>(pg => pg.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
