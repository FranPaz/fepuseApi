﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using fepuseAPI.ClasesAuxiliares;
using fepuseAPI.Models;

namespace fepuseAPI.Controllers
{
    public class FechasController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Fechas
        //iafar: trae todas las fechas de un torneo
        [Route("api/FechasTorneo")]
        public IHttpActionResult GetFechas(int prmIdTorneo)
        {
            try
            {
                var fechasTorneo = (from f in db.Fechas
                                    where f.torneoId == prmIdTorneo
                                    select f)
                                      .ToList();
                List<InfoFecha> infoFechasList = new List<InfoFecha>();

                foreach (var ft in fechasTorneo)
                {
                    Fecha fecha = (from f in db.Fechas
                                   where f.Id == ft.Id
                                   select f)
                               .Include(p => p.Partidos)
                               .Include(a => a.Partidos.Select(ar => ar.Arbitro))
                               .FirstOrDefault();


                    InfoFecha infoFecha = new InfoFecha //instancio la clase auxiliar para mostrar la info de los partidos de la fecha
                    {
                        Id = fecha.Id,
                        NumFecha = fecha.NumFecha,
                        torneoId = fecha.torneoId
                    };

                    List<InfoPartidoFixtureFecha> partidos = new List<InfoPartidoFixtureFecha>();

                    foreach (var item in fecha.Partidos)
                    {
                        InfoPartidoFixtureFecha p = new InfoPartidoFixtureFecha
                        {
                            Id = item.Id,
                            Dia = item.Dia,
                            Hora = item.Hora,
                            //Sede = item.Sede,
                            Sede = db.Sedes.Find(item.SedeId).Nombre,
                            GolesLocal = item.GolesLocal,
                            GolesVisitante = item.GolesVisitante,
                            nombreEquipoLocal = db.Equipoes.Find(item.EquipoLocalId).Nombre,
                            nombreEquipoVisitante = db.Equipoes.Find(item.EquipoVisitanteId).Nombre,
                            nombreArbitro = (from a in db.Arbitroes
                                             where a.Id == item.ArbitroId
                                             select a.NombreApellido).FirstOrDefault()
                        };

                        partidos.Add(p);


                        infoFecha.InfoPartidos = partidos;

                    }
                    infoFechasList.Add(infoFecha);
                }

                return Ok(infoFechasList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // GET: api/Fechas/5
        //fpaz: trae la info de la fecha y todos sus partidos (sin el detalle del partido)
        public IHttpActionResult GetFecha(int id)
        {
            try
            {
                Fecha fecha = (from f in db.Fechas
                               where f.Id == id
                               select f)
                               .Include(p => p.Partidos)
                               .Include(a => a.Partidos.Select(ar => ar.Arbitro))
                               .FirstOrDefault();

                if (fecha == null)
                {
                    return NotFound();
                }
                else
                {
                    InfoFecha infoFecha = new InfoFecha //instancio la clase auxiliar para mostrar la info de los partidos de la fecha
                    {
                        Id = fecha.Id,
                        NumFecha = fecha.NumFecha,
                        torneoId = fecha.torneoId
                    };

                    List<InfoPartidoFixtureFecha> partidos = new List<InfoPartidoFixtureFecha>();

                    foreach (var item in fecha.Partidos)
                    {
                        InfoPartidoFixtureFecha p = new InfoPartidoFixtureFecha
                        {
                            Id = item.Id,
                            Dia = item.Dia,
                            Hora = item.Hora,
                            //Sede = item.Sede,
                            Sede= db.Sedes.Find(item.SedeId).Nombre,
                            GolesLocal = item.GolesLocal,
                            GolesVisitante = item.GolesVisitante,
                            nombreEquipoLocal = db.Equipoes.Find(item.EquipoLocalId).Nombre,
                            nombreEquipoVisitante = db.Equipoes.Find(item.EquipoVisitanteId).Nombre,
                            nombreArbitro = (from a in db.Arbitroes
                                             where a.Id == item.ArbitroId
                                             select a.NombreApellido).FirstOrDefault()
                        };

                        partidos.Add(p);
                    }

                    infoFecha.InfoPartidos = partidos;

                    return Ok(infoFecha);
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //fpaz: devuelve el id de la primera fecha del Torneo
        public IHttpActionResult GetPrimeraFecha(int prmIdTorneo)
        {
            try
            {
                var primeraFechaTorneo = (from f in db.Fechas
                                          where f.torneoId == prmIdTorneo
                                          select f).FirstOrDefault();
                if (primeraFechaTorneo == null)
                {
                    return BadRequest("El torneo no tiene fechas cargadas");
                }

                return Ok(primeraFechaTorneo.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // PUT: api/Fechas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFecha(int id, Fecha fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fecha.Id)
            {
                return BadRequest();
            }

            db.Entry(fecha).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechaExists(id))
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

        // POST: api/Fechas
        [ResponseType(typeof(Fecha))]
        public IHttpActionResult PostFecha(Fecha fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var partido in fecha.Partidos)
                {
                    List<PartidoJugador> jugadoresPartido = new List<PartidoJugador>();

                    // obetengo el listado de jugadores del equipo local cargados para el torneo en particular
                    var jugadoresLocales = (from j in db.EquiposJugadorTorneos
                                            where j.EquipoTorneo.ZonaTorneo.TorneoId == fecha.torneoId
                                            && j.EquipoTorneo.EquipoId == partido.EquipoLocalId
                                            select j).ToList();

                    foreach (var item in jugadoresLocales)
                    {
                        var pj = new PartidoJugador()
                        {
                            JugadorId = item.JugadorId,
                            PartidoId = partido.Id,
                            EquipoId = item.EquipoTorneo.EquipoId,
                            Goles = 0,
                            TarjetasAmarillas = 0,
                            TarjetasRojas = 0
                        };
                        jugadoresPartido.Add(pj); // cargo al jugador como parte del partido                    
                    }

                    // obetengo el listado de jugadores del equipo visitantes cargados para el torneo en particular
                    var jugadoresVisitantes = (from j in db.EquiposJugadorTorneos
                                               where j.EquipoTorneo.ZonaTorneo.TorneoId == fecha.torneoId
                                               && j.EquipoTorneo.EquipoId == partido.EquipoVisitanteId
                                               select j).ToList();

                    foreach (var item in jugadoresVisitantes)
                    {
                        var pj = new PartidoJugador()
                        {
                            JugadorId = item.JugadorId,
                            PartidoId = partido.Id,
                            EquipoId = item.EquipoTorneo.EquipoId,
                            Goles = 0,
                            TarjetasAmarillas = 0,
                            TarjetasRojas = 0
                        };

                        jugadoresPartido.Add(pj); // cargo al jugador como parte del partido
                    }

                    partido.JugadoresDelPartido = jugadoresPartido;
                }

                db.Fechas.Add(fecha);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/Fechas/5
        [ResponseType(typeof(Fecha))]
        public IHttpActionResult DeleteFecha(int id)
        {
            Fecha fecha = db.Fechas.Find(id);
            if (fecha == null)
            {
                return NotFound();
            }

            db.Fechas.Remove(fecha);
            db.SaveChanges();

            return Ok(fecha);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FechaExists(int id)
        {
            return db.Fechas.Count(e => e.Id == id) > 0;
        }
    }

}