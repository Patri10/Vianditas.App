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
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.Nombre)
                .HasMaxLength(100);

            builder.Property(u => u.NumeroWhatsapp)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(u => u.NumeroWhatsapp)
                .IsUnique();

            builder.Property(u => u.WhatsappUserId)
                .HasMaxLength(64)
                .IsRequired();

            builder.HasIndex(u => u.WhatsappUserId)
                .IsUnique();
        }
    }
}
