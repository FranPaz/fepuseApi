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
using fepuseAPI.ClasesAuxiliares;
using fepuseAPI.Models;

namespace fepuseAPI.Controllers
{
    public class EquipoesController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Equipoes
        public IHttpActionResult GetEquipoes(int prmIdLiga) //fpaz: trae los datos de todos los equipos de la liga agrupados por categoria
        {
            try
            {                  
                var listEquipos = (from c in db.Categorias //obtengo las categorias y todos sus equipos
                                   where c.LigaId == prmIdLiga
                                   select c)
                                   .Include(e=>e.Equipos.Select(i => i.ImagenesEquipo))                                                                      
                                   .ToList();

                if (listEquipos == null)
                {
                    return BadRequest("No existen equipos cargados");
                }

                #region fpaz: para cada Equipo solo muestro la ultima imagen cargada como logo
                foreach (var categoria in listEquipos)
                {
                    foreach (var item in categoria.Equipos)
                    {
                        ImagenEquipo ultimaImagen = item.ImagenesEquipo.LastOrDefault();
                        List<ImagenEquipo> imagenes = new List<ImagenEquipo>();
                        imagenes.Add(ultimaImagen);

                        item.ImagenesEquipo = imagenes;                        
                    }                    
                }
                #endregion

                return Ok(listEquipos);
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult GetEquipos(int prmIdTorneo) //fpaz: trae los datos de todos los esquipos del torneo
        {
            try
            {
                var listEquipos = (from l in db.EquipoTorneos
                                   where l.ZonaTorneo.TorneoId == prmIdTorneo
                                   select l.Equipo)
                                   .Include(i => i.ImagenesEquipo);

                if (listEquipos == null)
                {
                    return BadRequest("No existen equipos cargados");
                }

                #region fpaz: para cada Equipo solo muestro la ultima imagen cargada como logo
                foreach (var item in listEquipos)
                {
                    ImagenEquipo ultimaImagen = item.ImagenesEquipo.LastOrDefault();
                    List<ImagenEquipo> imagenes = new List<ImagenEquipo>();
                    imagenes.Add(ultimaImagen);

                    item.ImagenesEquipo = imagenes;
                }
                #endregion

                return Ok(listEquipos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Equipoes/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult GetEquipo(int id) //fpaz: trae los datos de un equipo en particular, incluidos los jugadores
        {
            try
            {
                Equipo equipo = (from e in db.Equipoes
                                     where e.Id == id
                                     select e)
                                     .Include(j => j.EquiposJugadorTorneos.Select(s => s.Jugador))
                                     .Include(i=>i.ImagenesEquipo)
                                     .FirstOrDefault();
                if (equipo == null)
                {
                    return NotFound();
                }

                #region fpaz: obtengo la ultima imagen del equipo para el logo
                ImagenEquipo ultimaImagen = equipo.ImagenesEquipo.LastOrDefault();
                List<ImagenEquipo> imagenes = new List<ImagenEquipo>();
                imagenes.Add(ultimaImagen);
                equipo.ImagenesEquipo = imagenes;
                #endregion

                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            
        }

        // PUT: api/Equipoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipo(int id, Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipo.Id)
            {
                return BadRequest();
            }

            db.Entry(equipo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
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

        // POST: api/Equipoes
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult PostEquipo(Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Equipoes.Add(equipo);
                db.SaveChanges();

                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Route("api/Equipoes/Imagen")]
        public IHttpActionResult PostImagenEquipo(ImagenEquipo imagenEquipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.ImagenesEquipo.Add(imagenEquipo);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/Equipoes/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult DeleteEquipo(int id)
        {
            Equipo equipo = db.Equipoes.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            db.Equipoes.Remove(equipo);
            db.SaveChanges();

            return Ok(equipo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipoExists(int id)
        {
            return db.Equipoes.Count(e => e.Id == id) > 0;
        }
    }
}