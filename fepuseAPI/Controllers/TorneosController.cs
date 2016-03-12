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
                                   select t)
                                   .Include(i => i.ImagenesTorneo)
                                   .ToList();
                
                if (listTorneos == null)
                {
                    return BadRequest("No existen Torneos Cargados para la Liga");
                }

                foreach (var item in listTorneos) //fpaz: para cada torneo solo muestro la ultima imagen cargada como logo
                {
                    ImagenTorneo ultimaImagen = item.ImagenesTorneo.LastOrDefault();
                    List<ImagenTorneo> imagenes = new List<ImagenTorneo>();
                    imagenes.Add(ultimaImagen);

                    item.ImagenesTorneo = imagenes;                    
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
                                 .Include(e => e.EquipoTorneos
                                     .Select(eq => eq.Equipo.ImagenesEquipo)
                                     )
                                 .Include(f => f.Fechas)
                                 .Include(i=>i.ImagenesTorneo)
                                 .FirstOrDefault();

                if (torneo == null)
                {
                    return BadRequest("El torneo seleccionado no existe");
                }

                //fpaz: obtengo la ultima imagen del torneo para el logo
                ImagenTorneo ultimaImagen = torneo.ImagenesTorneo.LastOrDefault();
                List<ImagenTorneo> imagenes = new List<ImagenTorneo>();
                imagenes.Add(ultimaImagen);
                torneo.ImagenesTorneo = imagenes;
                
                //#region fpaz: obtengo los logos actuales de los equipos
                var equiposTorneo = torneo.EquipoTorneos.ToList();
                
                List<EquipoTorneo> equiposTorneoConImagen = new List<EquipoTorneo>();

                foreach (var item in equiposTorneo)
                {
                         ImagenEquipo ultimaImagenEquipo = item.Equipo.ImagenesEquipo.LastOrDefault();
                        List<ImagenEquipo> imagenesEquipo = new List<ImagenEquipo>();
                        imagenesEquipo.Add(ultimaImagenEquipo);

                        item.Equipo.ImagenesEquipo = imagenesEquipo;
  
                        equiposTorneoConImagen.Add(item);

                }

                torneo.EquipoTorneos = equiposTorneoConImagen;
                //#endregion


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
                        
            try
            {
                var torneoOrig = (from t in db.Torneos //obtengo los datos originales del tipo de habitacion que voy a modificar
                                  where t.Id == id
                                  select t)
                                      .Include(e => e.EquipoTorneos)
                                      .FirstOrDefault();

                if (torneoOrig != null)
                {
                    #region update de Equipos que juegan el torneo
                    var equiposOriginales = torneoOrig.EquipoTorneos;

                    // parte para carga de nuevos equipos al torneo
                    List<EquipoTorneo> equiposAgregados = new List<EquipoTorneo>();
                    foreach (var equipoAdd in torneo.EquipoTorneos)
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
                                    TorneoId = torneoOrig.Id
                                };

                                equiposAgregados.Add(et);
                            }
                        }
                    }


                    //parte para eliminacion de equipos
                    List<EquipoTorneo> equiposEliminados = new List<EquipoTorneo>();
                    foreach (var equipoOrig in equiposOriginales) // eliminacion de equipos que ya no estan en el array
                    {
                        var eo = (from e in torneo.EquipoTorneos // verifico si el equipo original esta en el obj modificado
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

                    torneoOrig.Nombre = torneo.Nombre;
                    torneoOrig.FechaInicio = torneo.FechaInicio;
                    torneoOrig.FechaFin = torneo.FechaFin;
                    torneoOrig.LigaId = torneo.LigaId;

                }
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);             
            }            
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

                return Ok(torneo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("api/Torneos/Imagen")]
        public IHttpActionResult PostImagenTorneo(ImagenTorneo imagenTorneo) //fpaz: asocia una imagen subida al azure al torneo
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.ImagenesTorneo.Add(imagenTorneo);
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