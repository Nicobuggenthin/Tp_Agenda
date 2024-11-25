using Agenda.Managers.Entidades;
using Agenda.Managers.Repos;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Agenda.Managers.Managers
{
    public interface IEventoManager
    {
        // Obtener todos los eventos
        IEnumerable<Evento> GetEventos();

        // Obtener un evento por su ID
        Evento GetEvento(int idEvento);

        // Crear un nuevo evento
        int CrearEvento(Evento evento);

        // Modificar un evento existente
        bool ModificarEvento(int idEvento, Evento evento);

        // Eliminar un evento
        bool EliminarEvento(int idEvento);
    }

    public class EventoManager : IEventoManager
    {
        private readonly IEventoRepository _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor que inyecta el repositorio y el IHttpContextAccessor
        public EventoManager(IEventoRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor; // Ahora se inyecta correctamente
        }

        // Obtener el UsuarioId desde el contexto
        private string ObtenerUsuarioId()
        {
            // Aseguramos que User esté presente en el contexto
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        // Obtener todos los eventos
        public IEnumerable<Evento> GetEventos()
        {
            string usuarioId = ObtenerUsuarioId(); // Obtener el ID del usuario actual
            var eventos = _repo.GetEventos(); // Obtener todos los eventos (sin filtro por usuario por ahora)
            //Debug.WriteLine("Eventos recuperados: " + eventos.Count()); // Esto te ayudará a ver si se están recuperando correctamente
            return eventos;
        }


        // Obtener un evento por su ID
        public Evento GetEvento(int idEvento)
        {
            return _repo.GetEvento(idEvento);
        }

        // Crear un nuevo evento
        public int CrearEvento(Evento evento)
        {
            // Aseguramos que se asignen FechaAlta y UsuarioId correctamente
            evento.FechaAlta = DateTime.Now; // Asignamos la fecha de alta
           // evento.UsuarioId = ObtenerUsuarioId(); // Aseguramos que el usuario esté asignado correctamente

            return _repo.CrearEvento(evento); // Llamamos al repositorio para crear el evento
        }

        // Modificar un evento existente
        public bool ModificarEvento(int idEvento, Evento evento)
        {
            // Obtenemos el evento original desde la base de datos
            var eventoEnDb = _repo.GetEvento(idEvento);

            if (eventoEnDb == null)
            {
                return false; // Si no existe el evento, no podemos modificarlo
            }

            // Asignamos los nuevos valores al evento
            eventoEnDb.Titulo = evento.Titulo;
            eventoEnDb.Fecha = evento.Fecha;
            eventoEnDb.Descripcion = evento.Descripcion;
            eventoEnDb.Ubicacion = evento.Ubicacion;
            eventoEnDb.Estado = evento.Estado;

            return _repo.ModificarEvento(idEvento, eventoEnDb); // Llamamos al repositorio para guardar los cambios
        }

        // Eliminar un evento
        public bool EliminarEvento(int idEvento)
        {
            string usuarioId = ObtenerUsuarioId(); // Obtener el UsuarioId desde el contexto
            return _repo.EliminarEvento(idEvento, usuarioId); // Llamamos al repositorio para eliminar el evento (soft delete)
        }
    }
}
