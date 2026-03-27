using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class ComercioConfiguration : IEntityTypeConfiguration<Comercio>
    {
        public void Configure(EntityTypeBuilder<Comercio> builder)
        {
            builder.ToTable("Comercios");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.Nombre)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.Direccion)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(c => c.Telefono)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.Activo)
                .HasDefaultValue(true);

            builder.HasMany(c => c.Menus)
                .WithOne(m => m.Comercio)
                .HasForeignKey(m => m.ComercioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
