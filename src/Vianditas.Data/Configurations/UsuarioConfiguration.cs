using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.Nombre)
                .HasMaxLength(100);

            builder.Property(u => u.Correo)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(u => u.Correo)
                .IsUnique();

            builder.Property(u => u.Contraseña)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
