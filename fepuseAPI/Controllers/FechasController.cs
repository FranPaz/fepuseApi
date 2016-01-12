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
    public class FechasController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Fechas
        public IQueryable<Fecha> GetFechas()
        {
            return db.Fechas;
        }

        // GET: api/Fechas/5
        [ResponseType(typeof(Fecha))]
        public IHttpActionResult GetFecha(int id)
        {
            Fecha fecha = db.Fechas.Find(id);
            if (fecha == null)
            {
                return NotFound();
            }

            return Ok(fecha);
        }

        // PUT: api/Fechas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFecha(int id, Fecha fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fecha.Id)
            {
                return BadRequest();
            }

            db.Entry(fecha).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechaExists(id))
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

        // POST: api/Fechas
        [ResponseType(typeof(Fecha))]
        public IHttpActionResult PostFecha(Fecha fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fechas.Add(fecha);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fecha.Id }, fecha);
        }

        // DELETE: api/Fechas/5
        [ResponseType(typeof(Fecha))]
        public IHttpActionResult DeleteFecha(int id)
        {
            Fecha fecha = db.Fechas.Find(id);
            if (fecha == null)
            {
                return NotFound();
            }

            db.Fechas.Remove(fecha);
            db.SaveChanges();

            return Ok(fecha);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FechaExists(int id)
        {
            return db.Fechas.Count(e => e.Id == id) > 0;
        }
    }
}