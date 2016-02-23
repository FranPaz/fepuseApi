using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using fepuseAPI.Models;

namespace fepuseAPI.Controllers
{
    public class PartidoesController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();
        
        // GET: api/Partidoes
        public IQueryable<Partido> GetPartidoes()
        {
            return db.Partidos;
         
        }

        // GET: api/Partidoes/5
        [ResponseType(typeof(Partido))]
        //fpaz: funcion para traer toda la info del partido, incluyendo equipo local y visitantes, con sus respectivos jugadore, arbitros, etc
        public IHttpActionResult GetPartido(int id)
        {
            try
            {
                Partido partido = (from p in db.Partidos
                                   where p.Id == id
                                   select p)
                                    .Include(el => el.EquipoLocal)
                                    .Include(el => el.EquipoVisitante)
                                   .Include(a => a.Arbitro)
                                  .Include(f => f.Fecha.Torneo)
                                  .Include(j => j.JugadoresDelPartido.Select(jug => jug.Jugador))
                                  .FirstOrDefault();




                if (partido == null)
                {
                    return NotFound();
                }

                //var jugadoresLocales = (from j in partido.EquipoLocal.EquiposJugadorTorneos
                //                                             where j.TorneoId == partido.Fecha.torneoId
                //                                             select j).ToList();

                //partido.EquipoLocal.EquiposJugadorTorneos = jugadoresLocales;

                //var jugadoresVistantes = (from j in partido.EquipoVisitante.EquiposJugadorTorneos
                //                        where j.TorneoId == partido.Fecha.torneoId
                //                        select j).ToList();

                //partido.EquipoVisitante.EquiposJugadorTorneos = jugadoresVistantes;
                return Ok(partido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Partidoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPartido(int id, Partido partido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != partido.Id)
            {
                return BadRequest();
            }

            try
            {
                var partidoOrig = (from p in db.Partidos //obtengo los datos originales del partido que voy a modificar
                                   where p.Id == id
                                   select p)
                                      .Include(jp => jp.JugadoresDelPartido)
                                      .FirstOrDefault();

                if (partidoOrig != null)
                {
                    #region update de Estadisticas de Jugadores del Partido
                    var jugadoresOriginales = partidoOrig.JugadoresDelPartido;

                    //parte para eliminacion de jugadores que no jugaron el partido
                    List<PartidoJugador> jugadoresEliminados = new List<PartidoJugador>();
                    foreach (var jo in jugadoresOriginales) // eliminacion de jugadores que ya no estan en el array
                    {
                        var jugOrig = (from jm in partido.JugadoresDelPartido // verifico si el jugador esta en el obj modificado
                                       where jm.Id == jo.Id
                                       select jm).FirstOrDefault();

                        if (jugOrig == null) // si no encontro la el jugador en el array modificado lo elimino del array
                        {
                            jugadoresEliminados.Add(jo);
                        }
                    }

                    //parte para actualizacion de datos estadisticos de cada jugador
                    foreach (var jo in jugadoresOriginales)
                    {
                        var jugMod = (from jm in partido.JugadoresDelPartido // verifico si la camaAdicional esta en el obj modificado
                                      where jm.Id == jo.Id
                                      select jm).FirstOrDefault();

                        if (jugMod != null) // si no encontro la cama adicional modifico los datos
                        {
                            jo.Goles = jugMod.Goles;
                            jo.TarjetasAmarillas = jugMod.TarjetasAmarillas;
                            jo.TarjetasRojas = jugMod.TarjetasRojas;
                        }
                    }

                    foreach (var item in jugadoresEliminados)
                    {
                        db.EstadisticasJugadors.Remove(item);
                    }

                    #endregion

                    //#region update de datos del Partido
                    partidoOrig.Sede = partido.Sede;
                    partidoOrig.DiaYHora = partido.DiaYHora;
                    partidoOrig.ArbitroId = partido.ArbitroId;
                    partidoOrig.GolesLocal = partido.GolesLocal;
                    partidoOrig.GolesVisitante = partido.GolesVisitante;
                    partidoOrig.Finalizado = partido.Finalizado;
                    //#endregion


                }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Partidoes
        [ResponseType(typeof(Partido))]
        public IHttpActionResult PostPartido(Partido partido) //fpaz: alta del partido y de los jugadores del partido
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var fecha = db.Fechas.Find(partido.FechaId); // obtengo info de la fecha en la que voy a cargar el partido
                List<PartidoJugador> jugadoresPartido = new List<PartidoJugador>();


                // obetengo el listado de jugadores del equipo local cargados para el torneo en particular
                var jugadoresLocales = (from j in db.EquiposJugadorTorneos
                                        where j.TorneoId == fecha.torneoId
                                        && j.EquipoId == partido.EquipoLocalId
                                        select j).ToList();

                foreach (var item in jugadoresLocales)
                {
                    var pj = new PartidoJugador()
                    {
                        JugadorId = item.JugadorId,
                        PartidoId = partido.Id,
                        EquipoId = item.EquipoId,
                        Goles = 0,
                        TarjetasAmarillas = 0,
                        TarjetasRojas = 0
                    };
                    jugadoresPartido.Add(pj); // cargo al jugador como parte del partido                    
                }

                // obetengo el listado de jugadores del equipo visitantes cargados para el torneo en particular
                var jugadoresVisitantes = (from j in db.EquiposJugadorTorneos
                                           where j.TorneoId == fecha.torneoId
                                           && j.EquipoId == partido.EquipoVisitanteId
                                           select j).ToList();

                foreach (var item in jugadoresVisitantes)
                {
                    var pj = new PartidoJugador()
                    {
                        JugadorId = item.JugadorId,
                        PartidoId = partido.Id,
                        EquipoId = item.EquipoId,
                        Goles = 0,
                        TarjetasAmarillas = 0,
                        TarjetasRojas = 0
                    };

                    jugadoresPartido.Add(pj); // cargo al jugador como parte del partido
                }

                partido.JugadoresDelPartido = jugadoresPartido;

                db.Partidos.Add(partido); //fpaz: carga del partido
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Partidoes/5
        [ResponseType(typeof(Partido))]
        public IHttpActionResult DeletePartido(int id)
        {
            Partido partido = db.Partidos.Find(id);
            if (partido == null)
            {
                return NotFound();
            }

            db.Partidos.Remove(partido);
            db.SaveChanges();

            return Ok(partido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartidoExists(int id)
        {
            return db.Partidos.Count(e => e.Id == id) > 0;
        }
    }
}