using Agenda.Managers.Managers;
using Agenda.Managers.Entidades;
using Agenda.Managers.Repos;
using Agenda.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace EventPlanner.Web.Controllers
{

    public class EventosController : Controller
    {
        private readonly IEventoManager _eventoManager;
        private readonly IEventoRepository _eventoRepository;
        public EventosController(IEventoManager eventoManager, IEventoRepository eventoRepository)
        {
            _eventoManager = eventoManager;
            _eventoRepository = eventoRepository;
        }

        // GET: EventosController
        public ActionResult Index()
        {
            var eventos = _eventoManager.GetEventos();
            return View(eventos);
        }

        // GET: EventosController/Details/5
        public ActionResult Details(int id)
        {
            var evento = _eventoManager.GetEvento(id);
            return View(evento);
        }

        // GET: EventosController/Create
        public ActionResult Create()
        {
            var model = new EventoModel
            {
                ListaEstadosItem = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Pendiente", Value = "Pendiente" },
                    new SelectListItem { Text = "Completado", Value = "Completado" },
                    new SelectListItem { Text = "Cancelado", Value = "Cancelado" }
                }
            };

            return View(model);
        }

        // POST: EventosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventoModel model)
        {
            if (ModelState.IsValid)
            {
                Debug.WriteLine($"Titulo: {model.Titulo}, Fecha: {model.Fecha}, Descripcion: {model.Descripcion}, Ubicacion: {model.Ubicacion}, Estado: {model.Estado}");

                var evento = new Evento
                {
                    Titulo = model.Titulo,
                    Fecha = model.Fecha,
                    Descripcion = model.Descripcion,
                    Ubicacion = model.Ubicacion,
                    Estado = model.Estado,
                   // UsuarioId = "defaultUser"
                };

                _eventoManager.CrearEvento(evento);
                return RedirectToAction(nameof(Index));
            }

            // Volver a cargar la lista de estados
            model.ListaEstadosItem = new List<SelectListItem>
    {
        new SelectListItem { Text = "Pendiente", Value = "Pendiente" },
        new SelectListItem { Text = "Completado", Value = "Completado" },
        new SelectListItem { Text = "Cancelado", Value = "Cancelado" }
    };

            return View(model);
        }


        // GET: EventosController/Edit/5
        public ActionResult Edit(int id)
        {
            var evento = _eventoManager.GetEvento(id);
            var model = new EventoModel
            {
                model = evento,
                ListaEstadosItem = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Pendiente", Value = "Pendiente" },
                    new SelectListItem { Text = "Completado", Value = "Completado" },
                    new SelectListItem { Text = "Cancelado", Value = "Cancelado" }
                }
            };
            return View(model);
        }

        // POST: EventosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventoModel model)
        {
            if (ModelState.IsValid)
            {
                var evento = new Evento
                {
                    Titulo = model.Titulo,
                    Fecha = model.Fecha,
                    Descripcion = model.Descripcion,
                    Ubicacion = model.Ubicacion,
                    Estado = model.Estado
                };

                _eventoManager.ModificarEvento(id, evento);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: EventosController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var evento = _eventoManager.GetEvento(id);
            return View(evento);
        }

        // POST: EventosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _eventoManager.EliminarEvento(id);
            return RedirectToAction(nameof(Index));
        }


        private string GetUserIdentityId()
        {
            // Busca el claim estándar del usuario autenticado
            var userId = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                ?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("No se pudo obtener el ID del usuario autenticado.");
            }

            return userId;
        }

    }
}
