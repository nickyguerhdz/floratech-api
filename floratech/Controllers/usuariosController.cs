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
    public class usuariosController : ApiController
    {

        private Model1 db = new Model1();

        // GET: api/usuarios
        public IQueryable<usuario> Getusuarios()
        {
            return db.usuarios;
        }

        // GET: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Getusuario(int id)
        {
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet]
        [Route("api/usuarios/GetTotalUsers")]
        public int GetTotalUsers()
        {
            return db.usuarios.Count();
        }

        [HttpPost]
        [Route("api/usuarios/login")]
        public IHttpActionResult Login(usuario user)
        {
            if (db.usuarios.Any(x => x.usuario1 == user.usuario1 && x.contrasena == user.contrasena))
            {
                user = db.usuarios.Where(x => x.usuario1 == user.usuario1 && x.contrasena == user.contrasena).FirstOrDefault();
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putusuario(int id, usuario usuario)
        {
            var findUsuario = db.usuarios.Where(x => x.id == id).FirstOrDefault();

            findUsuario.nombre = usuario.nombre;
            findUsuario.apellido = usuario.apellido;
            findUsuario.usuario1 = usuario.usuario1;
            findUsuario.contrasena = usuario.contrasena;


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(findUsuario);
        }

        // POST: api/usuarios
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Postusuario(usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.usuarios.Add(usuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuario.id }, usuario);
        }

        // DELETE: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Deleteusuario(int id)
        {
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usuarioExists(int id)
        {
            return db.usuarios.Count(e => e.id == id) > 0;
        }
    }
}