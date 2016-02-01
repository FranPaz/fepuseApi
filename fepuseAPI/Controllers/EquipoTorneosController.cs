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
    public class EquipoTorneosController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/EquipoTorneos
        public IQueryable<EquipoTorneo> GetEquipoTorneos()
        {
            return db.EquipoTorneos;
        }

        // GET: api/EquipoTorneos/5
        [ResponseType(typeof(EquipoTorneo))]
        public IHttpActionResult GetEquipoTorneo(int id) //fpaz: devuelve la tabla de posiciones para un torneo en particular
        {
            try
            {
                var tablaPosiciones = (from t in db.EquipoTorneos
                                       where t.TorneoId == id
                                       select t)
                                       .Include(e =>e.Equipo)
                                       .OrderByDescending(t => t.Puntos);

                if (tablaPosiciones == null)
                {
                    return NotFound();
                }

                return Ok(tablaPosiciones);
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }            
        }

        // PUT: api/EquipoTorneos/
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipoTorneo(List<EquipoTorneo> equiposTorneo)  //fpaz: actualiza los datos del torneo de un array de equipos
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            try
            {
                foreach (var item in equiposTorneo)
                {
                    //fpaz: obtengo los datos del equipo al que le voy a actualizar sus estadisticas
                    var datosEquipoTorneo = (from et in db.EquipoTorneos
                                             where et.TorneoId == item.TorneoId
                                             && et.EquipoId == item.EquipoId
                                             select et).FirstOrDefault();

                    //fpaz: actualizo los datos del equipo en el torneo
                    datosEquipoTorneo.Puntos += item.Puntos;
                    datosEquipoTorneo.PartidosJugados += item.PartidosJugados;
                    datosEquipoTorneo.PartidosGanados += item.PartidosGanados;
                    datosEquipoTorneo.PartidosEmpatados += item.PartidosEmpatados;
                    datosEquipoTorneo.PartidosPerdidos += item.PartidosPerdidos;
                    datosEquipoTorneo.GolesAFavor += item.GolesAFavor;
                    datosEquipoTorneo.GolesEnContra += item.GolesEnContra;
                    datosEquipoTorneo.DiferenciaGoles = item.GolesAFavor - datosEquipoTorneo.GolesEnContra;                    
                }
                
                db.SaveChanges();
                return Ok();
            }            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
      
        // POST: api/EquipoTorneos
        [ResponseType(typeof(EquipoTorneo))]
        public IHttpActionResult PostEquipoTorneo(EquipoTorneo equipoTorneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EquipoTorneos.Add(equipoTorneo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equipoTorneo.Id }, equipoTorneo);
        }

        // DELETE: api/EquipoTorneos/5
        [ResponseType(typeof(EquipoTorneo))]
        public IHttpActionResult DeleteEquipoTorneo(int id)
        {
            EquipoTorneo equipoTorneo = db.EquipoTorneos.Find(id);
            if (equipoTorneo == null)
            {
                return NotFound();
            }

            db.EquipoTorneos.Remove(equipoTorneo);
            db.SaveChanges();

            return Ok(equipoTorneo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipoTorneoExists(int id)
        {
            return db.EquipoTorneos.Count(e => e.Id == id) > 0;
        }
    }
}