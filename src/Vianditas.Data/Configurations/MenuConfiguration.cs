using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(m => m.Nombre)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(m => m.Descripcion)
                .HasMaxLength(500);

            builder.Property(m => m.Precio)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(m => m.Activo)
                .HasDefaultValue(true);

            builder.HasOne(m => m.Comercio)
                .WithMany(c => c.Menus)
                .HasForeignKey(m => m.ComercioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Categoria)
                .WithMany()
                .HasForeignKey(m => m.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
