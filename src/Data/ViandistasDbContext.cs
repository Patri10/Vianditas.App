using Microsoft.EntityFrameworkCore;
using Vianditas.Domain.model;
using Vianditas.Data.Configurations;

namespace Vianditas.Data
{
    public class ViandistasDbContext : DbContext
    {
        public ViandistasDbContext(DbContextOptions<ViandistasDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Detalle_Pedido> DetallePedidos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comercio> Comercios { get; set; }

        public DbSet<Disponibilidad_Diaria> DisponibilidadesDiarias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("pgcrypto");

            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new DetallePedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PagoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new ComercioConfiguration());
            modelBuilder.ApplyConfiguration(new DisponibilidadDiariaConfiguration());
        }
    }
}
