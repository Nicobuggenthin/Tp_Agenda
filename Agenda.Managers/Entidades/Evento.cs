using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


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
        public int IdEvento { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaModificacion { get; set; }
        //public string UsuarioId { get; set; }
        public DateTime? FechaBaja { get; set; }

        // Para el dropdown de Estados
        public List<SelectListItem> ListaEstadosItem { get; set; }

        // Usado para pasar el evento completo de vuelta
        public Evento Model { get; set; }
    }
}
