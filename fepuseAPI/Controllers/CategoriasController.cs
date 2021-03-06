﻿using System;
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
    public class CategoriasController : ApiController
    {
        private FepuseAPI_Context db = new FepuseAPI_Context();

        // GET: api/Categorias
        public IHttpActionResult GetCategorias(int prmIdLiga)
        {
            try
            {
               var listCategorias = (from c in db.Categorias //obtengo las categorias de la Liga
                                   where c.LigaId == prmIdLiga
                                   select c)                                   
                                   .ToList();
               
                if (listCategorias == null)
               {
                   return BadRequest("No existen Categorias cargadas");
               }
                else
                {
                    return Ok(listCategorias);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult GetCategoria(int id)
        {
            try
            {
                Categoria categoria = (from c in db.Categorias
                                       where c.Id == id
                                       select c)
                                      .Include(e => e.Equipos.Select(i => i.ImagenesEquipo))
                                      .FirstOrDefault();

                if (categoria == null)
                {
                    return NotFound();
                }

                #region fpaz: para cada Equipo solo muestro la ultima imagen cargada como logo
                foreach (var item in categoria.Equipos)
                {
                    ImagenEquipo ultimaImagen = item.ImagenesEquipo.LastOrDefault();
                    List<ImagenEquipo> imagenes = new List<ImagenEquipo>();
                    imagenes.Add(ultimaImagen);

                    item.ImagenesEquipo = imagenes;
                }
                #endregion

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // PUT: api/Categorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoria(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.Id)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult PostCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorias.Add(categoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoria.Id }, categoria);
        }

        // DELETE: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult DeleteCategoria(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(categoria);
            db.SaveChanges();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int id)
        {
            return db.Categorias.Count(e => e.Id == id) > 0;
        }
    }
}