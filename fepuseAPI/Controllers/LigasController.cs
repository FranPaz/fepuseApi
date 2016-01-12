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
    public class LigasController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Ligas
        public IQueryable<Liga> GetLigas()
        {
            return db.Ligas;
        }

        // GET: api/Ligas/5
        [ResponseType(typeof(Liga))]
        public IHttpActionResult GetLiga(int id)
        {
            Liga liga = db.Ligas.Find(id);
            if (liga == null)
            {
                return NotFound();
            }

            return Ok(liga);
        }

        // PUT: api/Ligas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLiga(int id, Liga liga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != liga.Id)
            {
                return BadRequest();
            }

            db.Entry(liga).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LigaExists(id))
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

        // POST: api/Ligas
        [ResponseType(typeof(Liga))]
        public IHttpActionResult PostLiga(Liga liga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ligas.Add(liga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = liga.Id }, liga);
        }

        // DELETE: api/Ligas/5
        [ResponseType(typeof(Liga))]
        public IHttpActionResult DeleteLiga(int id)
        {
            Liga liga = db.Ligas.Find(id);
            if (liga == null)
            {
                return NotFound();
            }

            db.Ligas.Remove(liga);
            db.SaveChanges();

            return Ok(liga);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LigaExists(int id)
        {
            return db.Ligas.Count(e => e.Id == id) > 0;
        }
    }
}