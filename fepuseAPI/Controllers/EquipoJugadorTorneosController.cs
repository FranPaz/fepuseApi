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
    public class EquipoJugadorTorneosController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/EquipoJugadorTorneos
        public IQueryable<EquipoJugadorTorneo> GetEquiposJugadorTorneos()
        {
            return db.EquiposJugadorTorneos;
        }

        // GET: api/EquipoJugadorTorneos/5
        [ResponseType(typeof(EquipoJugadorTorneo))]
        public IHttpActionResult GetEquipoJugadorTorneo(int id)
        {
            EquipoJugadorTorneo equipoJugadorTorneo = db.EquiposJugadorTorneos.Find(id);
            if (equipoJugadorTorneo == null)
            {
                return NotFound();
            }

            return Ok(equipoJugadorTorneo);
        }

        // PUT: api/EquipoJugadorTorneos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipoJugadorTorneo(int id, EquipoJugadorTorneo equipoJugadorTorneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipoJugadorTorneo.Id)
            {
                return BadRequest();
            }

            db.Entry(equipoJugadorTorneo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoJugadorTorneoExists(id))
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

        // POST: api/EquipoJugadorTorneos
        [ResponseType(typeof(EquipoJugadorTorneo))]
        public IHttpActionResult PostEquipoJugadorTorneo(EquipoJugadorTorneo equipoJugadorTorneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EquiposJugadorTorneos.Add(equipoJugadorTorneo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equipoJugadorTorneo.Id }, equipoJugadorTorneo);
        }

        // DELETE: api/EquipoJugadorTorneos/5
        [ResponseType(typeof(EquipoJugadorTorneo))]
        public IHttpActionResult DeleteEquipoJugadorTorneo(int id)
        {
            EquipoJugadorTorneo equipoJugadorTorneo = db.EquiposJugadorTorneos.Find(id);
            if (equipoJugadorTorneo == null)
            {
                return NotFound();
            }

            db.EquiposJugadorTorneos.Remove(equipoJugadorTorneo);
            db.SaveChanges();

            return Ok(equipoJugadorTorneo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipoJugadorTorneoExists(int id)
        {
            return db.EquiposJugadorTorneos.Count(e => e.Id == id) > 0;
        }
    }
}