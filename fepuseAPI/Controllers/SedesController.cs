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
    public class SedesController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Sedes
        public IQueryable<Sede> GetSedes()
        {
            return db.Sedes;
        }

        // GET: api/Sedes/5
        [ResponseType(typeof(Sede))]
        public IHttpActionResult GetSede(int id)
        {
            Sede sede = db.Sedes.Find(id);
            if (sede == null)
            {
                return NotFound();
            }

            return Ok(sede);
        }

        // PUT: api/Sedes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSede(int id, Sede sede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sede.Id)
            {
                return BadRequest();
            }

            db.Entry(sede).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SedeExists(id))
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

        // POST: api/Sedes
        [ResponseType(typeof(Sede))]
        public IHttpActionResult PostSede(Sede sede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sedes.Add(sede);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sede.Id }, sede);
        }

        // DELETE: api/Sedes/5
        [ResponseType(typeof(Sede))]
        public IHttpActionResult DeleteSede(int id)
        {
            Sede sede = db.Sedes.Find(id);
            if (sede == null)
            {
                return NotFound();
            }

            db.Sedes.Remove(sede);
            db.SaveChanges();

            return Ok(sede);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SedeExists(int id)
        {
            return db.Sedes.Count(e => e.Id == id) > 0;
        }
    }
}