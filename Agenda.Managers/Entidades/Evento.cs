using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Managers.Entidades
{
    public enum EstadoEvento
    {
        Pendiente,
        Realizado,
        NoRealizado
    }
    public class Evento
    {

        public int IdEvento { get; set; } // Identificador único del evento
        public string Titulo { get; set; } = null!; // Título del evento
        public DateTime Fecha { get; set; } // Fecha del evento
        public string Descripcion { get; set; } = null!; // Descripción del evento
        public string Ubicacion { get; set; } = null!; // Ubicación del evento
        public EstadoEvento Estado { get; set; } // Estado del evento (Pendiente, Realizado, No realizado)
        public DateTime FechaAlta { get; set; } //Propiedad para la fecha alta
        public DateTime? FechaBaja { get; set; } // Fecha de baja (soft delete)
        public DateTime? FechaModificacion { get; set; } // Fecha de última modificación (puede ser null si no se ha modificado)
    }
}
