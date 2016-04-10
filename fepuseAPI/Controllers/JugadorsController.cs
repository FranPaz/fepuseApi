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
using fepuseAPI.ClasesAuxiliares;

namespace fepuseAPI.Controllers
{
    public class JugadorsController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Jugadors
        public IHttpActionResult GetJugadors(int prmIdEquipo, int prmIdTorneo)
        {
            try
            {
                var listJugadores = (from ejt in db.EquiposJugadorTorneos
                                     where (ejt.EquipoId == prmIdEquipo) && (ejt.TorneoId == prmIdTorneo)
                                     select ejt.Jugador)
                                     .Include(j => j.EquiposJugadorTorneos)
                                     .Include(i=>i.ImagenesPersona)
                                     .ToList();

                #region fpaz: para cada jugador solo muestro la ultima imagen
                foreach (var item in listJugadores)
                {
                    ImagenPersona ultimaImagen = item.ImagenesPersona.LastOrDefault();
                    List<ImagenPersona> imagenes = new List<ImagenPersona>();
                    imagenes.Add(ultimaImagen);

                    item.ImagenesPersona = imagenes;
                }
                #endregion

                return Ok(listJugadores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        // GET: api/Jugadors/5
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult GetJugador(int dni)
        {
            //Jugador jugador = db.Jugadors.Find(id);
            Jugador jugador = (from e in db.Jugadors
                               where (e.Dni == dni)
                               select e)
                               .Include(ejt => ejt.EquiposJugadorTorneos)
                               .Include(i => i.ImagenesPersona )
                               .FirstOrDefault();

            if (jugador == null)
            {
                return NotFound();
            }

            #region fpaz: obtengo la ultima imagen del jugador
            ImagenPersona ultimaImagen = jugador.ImagenesPersona.LastOrDefault();
            List<ImagenPersona> imagenes = new List<ImagenPersona>();
            imagenes.Add(ultimaImagen);
            jugador.ImagenesPersona = imagenes;
            #endregion

            return Ok(jugador);
        }

        [Route("api/Jugadors/jugadoresliga")]
        public IHttpActionResult GetJugadoresLiga(int prmLigaId) // fpaz: lista todos los jugadores de la liga
        {
            try
            {
                // obtengo los goeadores del torneo
                List<Jugador> jugadores = (from t in db.EquiposJugadorTorneos
                                           join l in db.Ligas on t.Torneo.Categoria.LigaId equals l.Id
                                           where l.Id == prmLigaId
                                           group t by new
                                           {
                                               t.Jugador
                                           } into g
                                           select g.Key.Jugador)
                                               .ToList();

                return Ok(jugadores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Jugadors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJugador(int id, Jugador jugador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jugador.Id)
            {
                return BadRequest();
            }
            //#region Cambio el jugador de equipo

            db.Entry(jugador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JugadorExists(id))
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

        // POST: api/Jugadors
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult PostJugador(Jugador jugador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ejt = new EquipoJugadorTorneo
                {
                    EquipoId = (from e in jugador.EquiposJugadorTorneos
                                select e.EquipoId).FirstOrDefault(),
                    TorneoId = (from e in jugador.EquiposJugadorTorneos
                                select e.TorneoId).FirstOrDefault(),



                    Jugador = new Jugador
                    {
                        Dni = jugador.Dni,
                        NombreApellido = jugador.NombreApellido,
                        Direccion = jugador.Direccion,
                        Telefono = jugador.Telefono,
                        Email = jugador.Email,
                        Matricula = jugador.Matricula,
                        Apodo = jugador.Apodo,
                        Federado = jugador.Federado,
                        ProfesionId = jugador.ProfesionId,
                        FichaMedica = jugador.FichaMedica,
                        Profesion = (from pro in db.Profesions
                                     where pro.Id == jugador.ProfesionId
                                     select pro).FirstOrDefault()
                    }

                };

                db.EquiposJugadorTorneos.Add(ejt);

                //db.Jugadors.Add(jugador);
                db.SaveChanges();

                return Ok(ejt.Jugador);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Jugadors/Imagen")]
        public IHttpActionResult PostImagenPersona(ImagenPersona imagenPersona) //fpaz: asocia una imagen subida al azure al jugador
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.ImagenesPersona.Add(imagenPersona);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/Jugadors/5
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult DeleteJugador(int id)
        {
            Jugador jugador = db.Jugadors.Find(id);
            if (jugador == null)
            {
                return NotFound();
            }

            db.Jugadors.Remove(jugador);
            db.SaveChanges();

            return Ok(jugador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JugadorExists(int id)
        {
            return db.Jugadors.Count(e => e.Id == id) > 0;
        }
    }
}