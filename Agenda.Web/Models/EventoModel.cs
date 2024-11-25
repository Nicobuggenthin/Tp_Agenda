using Agenda.Managers.Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Agenda.Web.Models
{
    public class EventoModel
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [StringLength(400)]
        public string Descripcion { get; set; }

        [StringLength(200)]
        public string Ubicacion { get; set; }

        [Required]
        public string Estado { get; set; }

        // Lista de estados para el dropdown
        public List<SelectListItem> ListaEstadosItem { get; set; }

        // Puedes agregar más propiedades si es necesario, por ejemplo, para manejar el Evento completo.
        public Evento model { get; set; }
    }
}