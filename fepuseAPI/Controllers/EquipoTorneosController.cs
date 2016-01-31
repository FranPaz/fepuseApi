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
    public class EquipoTorneosController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/EquipoTorneos
        public IQueryable<EquipoTorneo> GetEquipoTorneos()
        {
            return db.EquipoTorneos;
        }

        // GET: api/EquipoTorneos/5
        [ResponseType(typeof(EquipoTorneo))]
        public IHttpActionResult GetEquipoTorneo(int id)
        {
            EquipoTorneo equipoTorneo = db.EquipoTorneos.Find(id);
            if (equipoTorneo == null)
            {
                return NotFound();
            }

            return Ok(equipoTorneo);
        }

        // PUT: api/EquipoTorneos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipoTorneo(int id, EquipoTorneo equipoTorneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipoTorneo.Id)
            {
                return BadRequest();
            }

            db.Entry(equipoTorneo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoTorneoExists(id))
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

        // POST: api/EquipoTorneos
        [ResponseType(typeof(EquipoTorneo))]
        public IHttpActionResult PostEquipoTorneo(EquipoTorneo equipoTorneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EquipoTorneos.Add(equipoTorneo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equipoTorneo.Id }, equipoTorneo);
        }

        // DELETE: api/EquipoTorneos/5
        [ResponseType(typeof(EquipoTorneo))]
        public IHttpActionResult DeleteEquipoTorneo(int id)
        {
            EquipoTorneo equipoTorneo = db.EquipoTorneos.Find(id);
            if (equipoTorneo == null)
            {
                return NotFound();
            }

            db.EquipoTorneos.Remove(equipoTorneo);
            db.SaveChanges();

            return Ok(equipoTorneo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipoTorneoExists(int id)
        {
            return db.EquipoTorneos.Count(e => e.Id == id) > 0;
        }
    }
}