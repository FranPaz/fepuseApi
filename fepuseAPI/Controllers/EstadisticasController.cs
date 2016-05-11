using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using fepuseAPI.ClasesAuxiliares;

namespace fepuseAPI.Controllers
{
    public class EstadisticasController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Estadisticas/5        
        public IHttpActionResult GetEstadicticasTorneo(int id) // fpaz: las estadisticas de de un torneo en particular
        {
            try
            {
                // obtengo los goeadores del torneo
                List<Goleadores> goleadores = (from ej in db.EstadisticasJugadors
                                               where ej.Partido.Fecha.torneoId == id
                                               && ej.Partido.Finalizado == true
                                               group ej by new
                                               {
                                                   ej.Jugador
                                               } into g
                                               select new Goleadores
                                               {
                                                   JugadorId = g.Key.Jugador.Id,
                                                   NombreJugador = g.Key.Jugador.NombreApellido,
                                                   PartidosJugados = g.Count(),
                                                   Goles = g.Sum(p => p.Goles),
                                                   NombreEquipo = (from eq in db.EquiposJugadorTorneos
                                                                   where eq.JugadorId == g.Key.Jugador.Id
                                                                   && eq.EquipoTorneo.ZonaTorneo.TorneoId == id
                                                                   select eq.EquipoTorneo.Equipo.Nombre).FirstOrDefault(),
                                               })
                                               .OrderByDescending(g => g.Goles)
                                               .Take(10)
                                               .ToList();

                // obtengo los Amonestados del torneo
                List<Amonestados> amonestados = (from ej in db.EstadisticasJugadors
                                                 where ej.Partido.Fecha.torneoId == id
                                                 && ej.Partido.Finalizado == true
                                                 group ej by new
                                                 {
                                                     ej.Jugador
                                                 } into g
                                                 select new Amonestados
                                                 {
                                                     JugadorId = g.Key.Jugador.Id,
                                                     NombreJugador = g.Key.Jugador.NombreApellido,
                                                     PartidosJugados = g.Count(),
                                                     CantTarjAmarillas = g.Sum(p => p.TarjetasAmarillas),
                                                     NombreEquipo = (from eq in db.EquiposJugadorTorneos
                                                                     where eq.JugadorId == g.Key.Jugador.Id
                                                                     && eq.EquipoTorneo.ZonaTorneo.TorneoId == id
                                                                     select eq.EquipoTorneo.Equipo.Nombre).FirstOrDefault(),
                                                 })
                                               .OrderByDescending(g => g.CantTarjAmarillas)
                                               .Take(10)
                                               .ToList();

                // obtengo los Esxpulsados del torneo
                List<Expulsados> expulsados = (from ej in db.EstadisticasJugadors
                                               where ej.Partido.Fecha.torneoId == id
                                               && ej.Partido.Finalizado == true
                                               group ej by new
                                               {
                                                   ej.Jugador
                                               } into g
                                               select new Expulsados
                                               {
                                                   JugadorId = g.Key.Jugador.Id,
                                                   NombreJugador = g.Key.Jugador.NombreApellido,
                                                   PartidosJugados = g.Count(),
                                                   CantTarjRojas = g.Sum(p => p.TarjetasRojas),
                                                   NombreEquipo = (from eq in db.EquiposJugadorTorneos
                                                                   where eq.JugadorId == g.Key.Jugador.Id
                                                                   && eq.EquipoTorneo.ZonaTorneo.TorneoId == id
                                                                   select eq.EquipoTorneo.Equipo.Nombre).FirstOrDefault(),
                                               })
                                               .OrderByDescending(g => g.CantTarjRojas)
                                               .Take(10)
                                               .ToList();


                var Estadicticas = new EstaditicasTorneo
                {
                    Torneo = db.Torneos.Find(id),
                    Goleadores = goleadores,
                    Amonestados = amonestados,
                    Expulsados = expulsados
                };


                return Ok(Estadicticas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
