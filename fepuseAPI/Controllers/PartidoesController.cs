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
        public IHttpActionResult GetPartido(int id)
        {
            Partido partido = db.Partidos.Find(id);
            if (partido == null)
            {
                return NotFound();
            }

            return Ok(partido);
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

            db.Entry(partido).State = EntityState.Modified;

            try
            {
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
        public IHttpActionResult PostPartido(Partido partido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Partidos.Add(partido);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = partido.Id }, partido);
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