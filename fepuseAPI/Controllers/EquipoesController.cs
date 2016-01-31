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
    public class EquipoesController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Equipoes
        public IHttpActionResult GetEquipoes(int prmIdLiga)
        {
            try
            {
                var listEquipos = (from l in db.Equipoes
                                   where l.LigaId == prmIdLiga
                                   select l);
                if (listEquipos == null)
                {
                    return BadRequest("No existen equipos cargados");
                }

                return Ok(listEquipos);
            }
            catch (Exception ex )
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
                                     .FirstOrDefault();
                if (equipo == null)
                {
                    return NotFound();
                }

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