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
                                   where t.Categoria.LigaId == prmIdLiga
                                   select t)
                                   .Include(i => i.ImagenesTorneo)
                                   .ToList();
                
                if (listTorneos == null)
                {
                    return BadRequest("No existen Torneos Cargados para la Liga");
                }

                #region fpaz: para cada torneo solo muestro la ultima imagen cargada como logo
                foreach (var item in listTorneos) 
                {
                    ImagenTorneo ultimaImagen = item.ImagenesTorneo.LastOrDefault();
                    List<ImagenTorneo> imagenes = new List<ImagenTorneo>();
                    imagenes.Add(ultimaImagen);

                    item.ImagenesTorneo = imagenes;
                }
                #endregion

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
                                 && t.Categoria.LigaId == prmIdLiga
                                 select t)
                                 .Include(c => c.Categoria)
                                 .Include(f => f.Fechas)
                                 .Include(i=>i.ImagenesTorneo)
                                 .Include(z => z.ZonasTorneo.Select(et=>et.EquiposTorneo.Select(e => e.Equipo)))
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
                                      .FirstOrDefault();

                if (torneoOrig != null)
                {
                    torneoOrig.Nombre = torneo.Nombre;
                    torneoOrig.FechaInicio = torneo.FechaInicio;
                    torneoOrig.FechaFin = torneo.FechaFin;
                    torneoOrig.CategoriaId= torneo.CategoriaId;

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