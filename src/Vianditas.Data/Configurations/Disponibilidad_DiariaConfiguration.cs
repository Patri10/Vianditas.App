using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vianditas.Domain.model;

namespace Vianditas.Data.Configurations
{
    public class DisponibilidadDiariaConfiguration : IEntityTypeConfiguration<Disponibilidad_Diaria>
    {
        public void Configure(EntityTypeBuilder<Disponibilidad_Diaria> builder)
        {
            builder.ToTable("DisponibilidadesDiarias");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(d => d.Fecha)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(d => d.Disponible)
                .IsRequired();

            builder.HasOne(d => d.Menu)
                .WithMany()
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(d => new { d.MenuId, d.Fecha })
                .IsUnique();


        }
    }
}