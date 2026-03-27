using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(c => c.Nombre)
                .IsUnique();
        }
    }
}
