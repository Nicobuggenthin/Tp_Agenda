using Agenda.Managers.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Agenda.Managers.Repos
{
    public interface IEventoRepository
    {
        Evento GetEvento(int idEvento);
        IEnumerable<Evento> GetEventos(bool? soloActivos = true);
        int CrearEvento(Evento evento);
        bool ModificarEvento(int idEvento, Evento evento);
        bool EliminarEvento(int idEvento, string idUsuarioBaja);
    }


    public class EventoRepository : IEventoRepository
    {
            private string _connectionString;

            public EventoRepository(string connectionString)
            {
                _connectionString = connectionString;
            }

            public Evento GetEvento(int idEvento)
            {
                using (IDbConnection conn = new SqlConnection(_connectionString))
                {
                    Evento result = conn.QuerySingle<Evento>("SELECT * FROM Eventos WHERE IdEventos = @IdEvento");
                    return result;
                }
            }

            public IEnumerable<Evento> GetEventos(bool? soloActivos = true)
            {
                using (IDbConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Eventos";
                    if (soloActivos == true)
                        query += " WHERE FechaBaja IS NULL";
                    return conn.Query<Evento>(query);
                }
            }
            //Crear Evento
            public int CrearEvento(Evento evento)
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @"
                    INSERT INTO Eventos (Titulo, Fecha, Descripcion, Ubicacion, Estado, FechaAlta, UsuarioId)
                    VALUES (@Titulo, @Fecha, @Descripcion, @Ubicacion, @Estado, @FechaAlta, @UsuarioId);
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";

                    evento.FechaAlta = DateTime.Now; // Asignamos la fecha de alta
                    return db.QuerySingle<int>(query, evento);
                }
            }
            //Modificar evento
            public bool ModificarEvento(int idEvento, Evento evento)
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @"
                    UPDATE Eventos
                    SET Titulo = @Titulo, Fecha = @Fecha, Descripcion = @Descripcion, 
                        Ubicacion = @Ubicacion, Estado = @Estado, FechaModificacion = @FechaModificacion
                    WHERE IdEventos = @IdEventos";

                    evento.FechaModificacion = DateTime.Now; // Fecha de modificación
                    return db.Execute(query, new { evento.Titulo, evento.Fecha, evento.Descripcion, evento.Ubicacion, evento.Estado, evento.FechaModificacion, IdEventos = idEvento }) > 0;
                }
            }

            // Eliminar evento (soft delete)
            public bool EliminarEvento(int idEvento, string idUsuarioBaja)
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string query = @"
                    UPDATE Eventos
                    SET FechaBaja = @FechaBaja, UsuarioIdBaja = @UsuarioIdBaja
                    WHERE IdEventos = @IdEvento";

                    // Actualizamos la fecha de baja y el idUsuarioBaja
                    var result = db.Execute(query, new { FechaBaja = DateTime.Now, UsuarioIdBaja = idUsuarioBaja, IdEvento = idEvento });
                    return result > 0; // Retorna true si se actualizó correctamente
                }
            }
        }
}
