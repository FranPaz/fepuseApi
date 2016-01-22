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
using fepuseAPI.Models;

namespace fepuseAPI.Controllers
{
    public class TorneosController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Torneos
        public IHttpActionResult GetTorneos(int prmIdLiga) //fpaz: devuelve todos los torneos de una liga en particular
        {
            try
            {
                var listTorneos = (from t in db.Torneos
                                   where t.LigaId == prmIdLiga
                                   select t).ToList();
                
                if (listTorneos == null)
                {
                    return BadRequest("No existen Torneos Cargados para la Liga");
                }

                return Ok(listTorneos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/Torneos/5
        [ResponseType(typeof(Torneo))]
        public IHttpActionResult GetTorneo(int prmIdLiga, int prmIdTorneo) // fpaz: trae los datos de un torneo en particular
        {
            try
            {
                Torneo torneo = (from t in db.Torneos
                                 where t.Id == prmIdTorneo
                                 && t.LigaId == prmIdLiga
                                 select t)
                                 .Include(e => e.Equipos)
                                 .FirstOrDefault();

                if (torneo == null)
                {
                    return BadRequest("El torneo seleccionado no existe");
                }

                return Ok(torneo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // PUT: api/Torneos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTorneo(int id, Torneo torneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != torneo.Id)
            {
                return BadRequest();
            }

//            db.Entry(torneo).State = EntityState.Modified;


            try
            {
                var torneoOrig = (from t in db.Torneos //obtengo los datos originales del tipo de habitacion que voy a modificar
                                   where t.Id == id
                                   select t)
                                      .Include(e => e.Equipos)                                      
                                      .FirstOrDefault();

                if (torneoOrig != null)
                {
                    #region update de Equipos que juegan el torneo
                    var equiposOriginales = torneoOrig.Equipos;

                    // parte para carga de nuevos equipos al torneo
                    List<Equipo> equiposAgregados = new List<Equipo>();
                    foreach (var equipoAdd in torneo.Equipos) 
                    {
                        var equipo = (from equipoOrig in equiposOriginales // verifico si el equipo esta en el obj modificado
                                      where equipoOrig.Id == equipoAdd.Id
                                      select equipoOrig).FirstOrDefault();

                        if (equipo == null) // si no encontro el equipo agrego al array para su carga
                        {
                            var eq = db.Equipoes.Find(equipoAdd.Id);
                            if (eq != null)
                            {
                                equiposAgregados.Add(eq);
                            }
                        }
                    }


                    //parte para eliminacion de equipos
                    List<Equipo> equiposEliminados = new List<Equipo>();
                    foreach (var equipoOrig in equiposOriginales) // eliminacion de equipos que ya no estan en el array
                    {
                        var eo = (from e in torneo.Equipos // verifico si el equipo original esta en el obj modificado
                                            where e.Id == equipoOrig.Id
                                            select e).FirstOrDefault();

                        if (eo == null) // si no encontro el equipo la elimino del array
                        {
                            equiposEliminados.Add(equipoOrig);
                        }
                    }

                    foreach (var item in equiposAgregados)
                    {
                        torneoOrig.Equipos.Add(item);                        
                    }

                    foreach (var item in equiposEliminados)
                    {
                        torneoOrig.Equipos.Remove(item);
                    }
                    #endregion




                }
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TorneoExists(id))
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

        // POST: api/Torneos
        [ResponseType(typeof(Torneo))]
        public IHttpActionResult PostTorneo(Torneo torneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Torneos.Add(torneo);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/Torneos/5
        [ResponseType(typeof(Torneo))]
        public IHttpActionResult DeleteTorneo(int id)
        {
            Torneo torneo = db.Torneos.Find(id);
            if (torneo == null)
            {
                return NotFound();
            }

            db.Torneos.Remove(torneo);
            db.SaveChanges();

            return Ok(torneo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TorneoExists(int id)
        {
            return db.Torneos.Count(e => e.Id == id) > 0;
        }
    }
}