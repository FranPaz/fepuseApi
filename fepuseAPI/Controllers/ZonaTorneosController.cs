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
    public class ZonaTorneosController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/ZonaTorneos
        public IHttpActionResult GetZonaTorneos(int prmIdTorneo) // fpaz devuelve todas las zonas de un torneo en particular y los equipos de cada zona
        {
            try
            {
                var listZonas = (from z in db.ZonaTorneos
                                 where z.TorneoId == prmIdTorneo
                                 select z)
                                 .Include(e => e.EquiposTorneo
                                        .Select(eq => eq.Equipo.ImagenesEquipo)
                                    )
                                 .ToList();
                if (listZonas == null)
                {
                    return BadRequest("No existen Zonas Cargadas Para el Torneo Seleccionado");
                }

                foreach (var zona in listZonas) //para cada zona agrego los logos actuales de los equipos
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

                return Ok(listZonas);
                
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/ZonaTorneos/5
        [ResponseType(typeof(ZonaTorneo))]
        public IHttpActionResult GetZonaTorneo(int id) // fpaz: devuelvo la info de una zona de un torneo en particular
        {
            try
            {
                ZonaTorneo zonaTorneo = (from z in db.ZonaTorneos
                                         where z.Id == id
                                         select z)
                                         .Include(t => t.Torneo)
                                        .Include(e => e.EquiposTorneo
                                            .Select(eq => eq.Equipo.ImagenesEquipo)
                                        )
                                        .FirstOrDefault();
                
                if (zonaTorneo == null)
                {
                    return NotFound();
                }
                else
                {
                    #region fpaz: obtengo los logos actuales de los equipos
                    var equiposTorneo = zonaTorneo.EquiposTorneo.ToList();

                    List<EquipoTorneo> equiposTorneoConImagen = new List<EquipoTorneo>();

                    foreach (var item in equiposTorneo)
                    {
                        ImagenEquipo ultimaImagenEquipo = item.Equipo.ImagenesEquipo.LastOrDefault();
                        List<ImagenEquipo> imagenesEquipo = new List<ImagenEquipo>();
                        imagenesEquipo.Add(ultimaImagenEquipo);

                        item.Equipo.ImagenesEquipo = imagenesEquipo;

                        equiposTorneoConImagen.Add(item);

                    }

                    zonaTorneo.EquiposTorneo = equiposTorneoConImagen;
                    #endregion
                }

                return Ok(zonaTorneo);
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }           
            
        }

        // PUT: api/ZonaTorneos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutZonaTorneo(int id, ZonaTorneo zonaTorneo) //fpaz: actualizacion de los datos de una zona de un torneo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zonaTorneo.Id)
            {
                return BadRequest();
            }

            try
            {
                var zonaOrig = (from z in db.ZonaTorneos //obtengo los datos originales de la zona que voy a modificar
                                  where z.Id == id
                                  select z)
                                  .Include(e => e.EquiposTorneo)
                                  .FirstOrDefault();

                if (zonaOrig != null)
                {
                    #region update de Equipos de la Zona que juegan el torneo
                    var equiposOriginales = zonaOrig.EquiposTorneo;

                    // parte para carga de nuevos equipos al torneo
                    List<EquipoTorneo> equiposAgregados = new List<EquipoTorneo>();
                    foreach (var equipoAdd in zonaTorneo.EquiposTorneo)
                    {
                        var equipo = (from equipoOrig in equiposOriginales // verifico si el equipo esta en el obj modificado
                                      where equipoOrig.EquipoId == equipoAdd.EquipoId
                                      select equipoOrig).FirstOrDefault();

                        if (equipo == null) // si no encontro el equipo agrego al array para su carga
                        {
                            var eq = db.Equipoes.Find(equipoAdd.EquipoId);
                            if (eq != null)
                            {
                                var et = new EquipoTorneo()
                                {
                                    EquipoId = eq.Id,
                                    ZonaTorneoId = zonaOrig.Id                                    
                                };

                                equiposAgregados.Add(et);
                            }
                        }
                    }


                    //parte para eliminacion de equipos
                    List<EquipoTorneo> equiposEliminados = new List<EquipoTorneo>();
                    foreach (var equipoOrig in equiposOriginales) // eliminacion de equipos que ya no estan en el array
                    {
                        var eo = (from e in zonaTorneo.EquiposTorneo // verifico si el equipo original esta en el obj modificado
                                  where equipoOrig.EquipoId == e.EquipoId
                                  select e).FirstOrDefault();

                        if (eo == null) // si no encontro el equipo la elimino del array
                        {
                            equiposEliminados.Add(equipoOrig);
                        }
                    }

                    foreach (var item in equiposAgregados)
                    {
                        db.EquipoTorneos.Add(item);
                        //torneoOrig.EquipoTorneos.Add(item);
                    }

                    foreach (var item in equiposEliminados)
                    {
                        db.EquipoTorneos.Remove(item);
                        //torneoOrig.EquipoTorneos.Remove(item);
                    }
                    #endregion

                    zonaOrig.Descripcion= zonaTorneo.Descripcion;                    
                    zonaOrig.TorneoId = zonaTorneo.TorneoId;

                }
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // POST: api/ZonaTorneos
        [ResponseType(typeof(ZonaTorneo))]
        public IHttpActionResult PostZonaTorneo(ZonaTorneo zonaTorneo) // funcion para agregar una nueva zona al torneo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.ZonaTorneos.Add(zonaTorneo);
                db.SaveChanges();

                return Ok(zonaTorneo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ZonaTorneos/5
        [ResponseType(typeof(ZonaTorneo))]
        public IHttpActionResult DeleteZonaTorneo(int id)
        {
            ZonaTorneo zonaTorneo = db.ZonaTorneos.Find(id);
            if (zonaTorneo == null)
            {
                return NotFound();
            }

            db.ZonaTorneos.Remove(zonaTorneo);
            db.SaveChanges();

            return Ok(zonaTorneo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZonaTorneoExists(int id)
        {
            return db.ZonaTorneos.Count(e => e.Id == id) > 0;
        }
    }
}