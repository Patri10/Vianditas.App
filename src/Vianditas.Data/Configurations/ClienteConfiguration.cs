using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Apellido)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Telefono)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Direccion)
                .HasMaxLength(255);

            // Aseguramos que no haya clientes duplicados por teléfono
            builder.HasIndex(c => c.Telefono).IsUnique();
        }
    }
}
