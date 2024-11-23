using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Agenda.Manager.Entidades
{
    public class Evento
    {
        public int IdEventos { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; } //Puede ser nulo
        public string? Ubicacion { get; set; } //Puede ser nulo
        public string UsuarioId { get; set; } //Relacion con la tabla ASPNetUsers
        public string Estado { get; set; } = "Pendiente"; //Valor por defecto

    }
}
