namespace Vianditas.Domain.model
{

    public class Disponibilidad_Diaria
    {
        private Disponibilidad_Diaria()
        {
        }

        public Guid Id { get; private set; }

        public DateTime Fecha { get; private set; }
        public bool Disponible { get; private set; }

        public Guid MenuId { get; private set; }
        public Menu Menu { get; private set; } = null!;

        public Disponibilidad_Diaria(DateTime fecha, bool disponible, Guid menuId)
        {
            Fecha = fecha;
            Disponible = disponible;
            MenuId = menuId;

        }
    }
}