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
using floratech.Models;

namespace floratech.Controllers
{
    public class favoritosController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/favoritos
        public IQueryable<favorito> Getfavoritos()
        {
            return db.favoritos;
        }

        // GET: api/favoritos/5
        [ResponseType(typeof(favorito))]
        public IHttpActionResult Getfavorito(int id)
        {
            favorito favorito = db.favoritos.Find(id);
            if (favorito == null)
            {
                return NotFound();
            }

            return Ok(favorito);
        }

        // PUT: api/favoritos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfavorito(int id, favorito favorito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != favorito.id)
            {
                return BadRequest();
            }

            db.Entry(favorito).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!favoritoExists(id))
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

        // POST: api/favoritos
        [ResponseType(typeof(favorito))]
        public IHttpActionResult Postfavorito(favorito favorito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.favoritos.Add(favorito);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = favorito.id }, favorito);
        }

        // DELETE: api/favoritos/5
        [ResponseType(typeof(favorito))]
        public IHttpActionResult Deletefavorito(int id)
        {
            favorito favorito = db.favoritos.Find(id);
            if (favorito == null)
            {
                return NotFound();
            }

            db.favoritos.Remove(favorito);
            db.SaveChanges();

            return Ok(favorito);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool favoritoExists(int id)
        {
            return db.favoritos.Count(e => e.id == id) > 0;
        }
    }
}