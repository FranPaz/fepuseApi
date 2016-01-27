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
    public class ArbitroesController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Arbitroes
        public IHttpActionResult GetArbitroes(int prmIdLiga)
        {
            try
            {
                var listArbitros = (from a in db.Arbitroes
                                    where a.LigaId == prmIdLiga
                                    select a).ToList();
                if (listArbitros == null)
                {
                    return BadRequest("No existen arbitros Cargados para la Liga");
                }
                else
                {
                    return Ok(listArbitros);
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Arbitroes/5
        [ResponseType(typeof(Arbitro))]
        public IHttpActionResult GetArbitro(int id)
        {
            Arbitro arbitro = db.Arbitroes.Find(id);
            if (arbitro == null)
            {
                return NotFound();
            }

            return Ok(arbitro);
        }

        // PUT: api/Arbitroes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArbitro(int id, Arbitro arbitro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arbitro.Id)
            {
                return BadRequest();
            }

            db.Entry(arbitro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArbitroExists(id))
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

        // POST: api/Arbitroes
        [ResponseType(typeof(Arbitro))]
        public IHttpActionResult PostArbitro(Arbitro arbitro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.Arbitroes.Add(arbitro);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // DELETE: api/Arbitroes/5
        [ResponseType(typeof(Arbitro))]
        public IHttpActionResult DeleteArbitro(int id)
        {
            Arbitro arbitro = db.Arbitroes.Find(id);
            if (arbitro == null)
            {
                return NotFound();
            }

            db.Arbitroes.Remove(arbitro);
            db.SaveChanges();

            return Ok(arbitro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArbitroExists(int id)
        {
            return db.Arbitroes.Count(e => e.Id == id) > 0;
        }
    }
}