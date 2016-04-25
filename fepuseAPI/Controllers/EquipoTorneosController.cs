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
                //var tablaPosiciones = (from t in db.EquipoTorneos
                //                       where t.ZonaTorneo.TorneoId == id
                //                       select t)
                //                       .Include(e =>e.Equipo.ImagenesEquipo)
                //                       .OrderByDescending(t => t.Puntos)
                //                       .ToList();

                var tablaPosiciones = (from z in db.ZonaTorneos
                                       where z.TorneoId == id
                                       select z)
                                       .Include(et => et.EquiposTorneo                                           
                                           .Select(e => e.Equipo.ImagenesEquipo)                                           
                                           )                                                                              
                                       .ToList();

                if (tablaPosiciones == null)
                {
                    return NotFound();
                }

                #region fpaz: para cada Equipo solo muestro la ultima imagen cargada como logo
                foreach (var zona in tablaPosiciones) //para cada zona agrego los logos actuales de los equipos
                {
                    #region fpaz: obtengo los logos actuales de los equipos
                    var equiposTorneo = zona.EquiposTorneo.ToList();

                    List<EquipoTorneo> equiposTorneoConImagen = new List<EquipoTorneo>();

                    foreach (var item in equiposTorneo)
                    {
                        ImagenEquipo ultimaImagenEquipo = item.Equipo.ImagenesEquipo.LastOrDefault();
                        List<ImagenEquipo> imagenesEquipo = new List<ImagenEquipo>();
                        imagenesEquipo.Add(ultimaImagenEquipo);

                        item.Equipo.ImagenesEquipo = imagenesEquipo;

                        equiposTorneoConImagen.Add(item);

                    }

                    zona.EquiposTorneo = equiposTorneoConImagen;
                    #endregion
                }
                #endregion

                #region fpaz: ordeno a los equipos de cada zona de manera descendene segun sus puntos
                foreach (var zona in tablaPosiciones)
                {
                    var tablaZona = zona.EquiposTorneo.OrderByDescending(p => p.Puntos).ToList();
                    zona.EquiposTorneo = tablaZona;

                }
                #endregion

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
                                             where et.ZonaTorneo.TorneoId == item.ZonaTorneo.TorneoId
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