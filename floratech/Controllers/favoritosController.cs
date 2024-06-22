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
        // GET: favoritos
        private Model1 db = new Model1();

        // GET: api/favoritos
        public IQueryable<favorito> Getfavoritos()
        {
            return db.favoritos;
        }

        // GET: api/favoritos/5
        [ResponseType(typeof(favorito))]
        public IHttpActionResult Getfavoritos(int id)
        {
            favorito favoritos = db.favoritos.Find(id);
            if (favoritos == null)
            {
                return NotFound();
            }

            return Ok(favoritos);
        }

        [HttpGet]
        [Route("api/favoritos/GetTotalFavoritos")]
        public int GetTotalFavoritos()
        {
            return db.favoritos.Count();
        }

        // GET: api/favoritos/byuser/1
        [HttpGet]
        [Route("api/favoritos/byuser/{usuario_id}")]
        [ResponseType(typeof(IEnumerable<favorito>))]
        public IHttpActionResult GetfavoritosByUsuarioId(int usuario_id)
        {
            var favoritos = db.favoritos.Where(f => f.usuario_id == usuario_id).ToList();

            if (!favoritos.Any())
            {
                return Ok(0);
            }

            return Ok(favoritos);
        }

        [HttpGet]
        [Route("api/favoritos/byuser/{usuario_id}/frutas")]
        [ResponseType(typeof(IEnumerable<favorito>))]
        public IHttpActionResult GetFavoritosFrutasByUsuarioId(int usuario_id)
        {
            var favoritos = db.favoritos.Where(f => f.usuario_id == usuario_id && f.tipo == "Fruta").ToList();

            if (!favoritos.Any())
            {
                return Ok(0);
            }

            return Ok(favoritos);
        }

        [HttpGet]
        [Route("api/favoritos/byuser/{usuario_id}/plantas")]
        [ResponseType(typeof(IEnumerable<favorito>))]
        public IHttpActionResult GetFavoritosPlantasByUsuarioId(int usuario_id)
        {
            var favoritos = db.favoritos.Where(f => f.usuario_id == usuario_id && f.tipo == "Planta").ToList();

            if (!favoritos.Any())
            {
                return Ok(0);
            }

            return Ok(favoritos);
        }

        [HttpGet]
        [Route("api/favoritos/byuser/{usuario_id}/plagas")]
        [ResponseType(typeof(IEnumerable<favorito>))]
        public IHttpActionResult GetFavoritosPlagasByUsuarioId(int usuario_id)
        {
            var favoritos = db.favoritos.Where(f => f.usuario_id == usuario_id && f.tipo == "Plaga").ToList();

            if (!favoritos.Any())
            {
                return Ok(0);
            }

            return Ok(favoritos);
        }


        [HttpPost]
        [Route("api/favoritos/getId")]
        [ResponseType(typeof(int?))]
        public IHttpActionResult GetFavoritoId(favorito favorito)
        {
            var existingFavorito = db.favoritos
                .Where(f => f.usuario_id == favorito.usuario_id &&
                            f.tipo == favorito.tipo &&
                            f.id_elementoAPI == favorito.id_elementoAPI &&
                            f.elemento == favorito.elemento)
                .Select(f => f.id)
                .FirstOrDefault();

            if (existingFavorito == 0)
            {
                return NotFound();
            }

            return Ok(existingFavorito);
        }


        // POST: api/favoritos
        [ResponseType(typeof(favorito))]
        public IHttpActionResult Postfavoritos(favorito favoritos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.favoritos.Add(favoritos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = favoritos.id }, favoritos);
        }

        // PUT: api/favoritos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfavoritos(int id, favorito favoritos)
        {
            var findFavoritos = db.favoritos.Where(x => x.id == id).FirstOrDefault();

            if (findFavoritos == null)
            {
                return NotFound();
            }

            findFavoritos.usuario_id = favoritos.usuario_id;
            findFavoritos.tipo = favoritos.tipo;
            findFavoritos.id_elementoAPI = favoritos.id_elementoAPI;
            findFavoritos.elemento = favoritos.elemento;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!favoritosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(findFavoritos);
        }

        // DELETE: api/favoritos/5
        [ResponseType(typeof(favorito))]
        public IHttpActionResult Deletefavoritos(int id)
        {
            favorito favoritos = db.favoritos.Find(id);
            if (favoritos == null)
            {
                return NotFound();
            }

            db.favoritos.Remove(favoritos);
            db.SaveChanges();

            return Ok(favoritos);
        }

        [HttpPost]
        [Route("api/favoritos/check")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckFavorito(favorito favorito)
        {
            var exists = db.favoritos.Any(f => f.usuario_id == favorito.usuario_id && f.tipo == favorito.tipo && f.id_elementoAPI == favorito.id_elementoAPI && f.elemento == favorito.elemento);
            return Ok(exists);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool favoritosExists(int id)
        {
            return db.favoritos.Count(e => e.id == id) > 0;
        }
    }

}
