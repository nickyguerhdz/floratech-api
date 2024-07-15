using floratech.Model;
using floratech.Connection;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

[EnableCors("*", "*", "*")]
[RoutePrefix("api/usuarios")]
public class UsuariosController : ApiController
{
    private readonly IMongoCollection<Usuarios> _usuariosCollection;

    private async Task<int> GetNextUserIdAsync()
    {
        var sort = Builders<Usuarios>.Sort.Descending(u => u.userId);
        var highestUserId = await _usuariosCollection.Find(_ => true).Sort(sort).Limit(1).FirstOrDefaultAsync();

        return highestUserId != null ? highestUserId.userId + 1 : 1;
    }


    public UsuariosController()
    {
        mongo_db db_mongo = new mongo_db("floratech", "floratech1", "floratech-db", "maincluster.ixylg4p.mongodb.net/");

        _usuariosCollection = db_mongo.mongoDatabase.GetCollection<Usuarios>("usuarios");
    }

    // GET api/usuarios
    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<Usuarios>> Get()
    {
        return await _usuariosCollection.Find(new BsonDocument()).ToListAsync();
    }

    // GET api/usuarios/{userId}
    [HttpGet]
    [Route("{userId}")]
    public async Task<IHttpActionResult> GetHistorialByUserId(int userId)
    {
        var usuario = await _usuariosCollection.Find(x => x.userId == userId).FirstOrDefaultAsync();
        if (usuario == null)
        {
            return Ok("Usuario vacío.");
        }
        return Ok(usuario);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IHttpActionResult> GetAllHistorial()
    {
        var usuarios = await _usuariosCollection
            .Find(_ => true)
            .ToListAsync();

        var totalUsuarios = usuarios.Count;

        return Ok(new
        {
            usuarios,
            totalUsuarios
        });
    }

    // POST api/usuarios
    [HttpPost]
    [Route("")]
    public async Task<IHttpActionResult> Post([FromBody] Usuarios usuario)
    {
        if (usuario == null)
        {
            return BadRequest("Usuario no proporcionado.");
        }

        usuario.userId = await GetNextUserIdAsync();

        await _usuariosCollection.InsertOneAsync(usuario);
        return Ok(usuario);
    }

    // POST api/usuarios/login
    [HttpPost]
    [Route("login")]
    public async Task<IHttpActionResult> Login([FromBody] Usuarios usuario)
    {
        if (usuario == null || string.IsNullOrEmpty(usuario.nombre_usuario) || string.IsNullOrEmpty(usuario.contrasena))
        {
            return Ok("Usuario o contraseña no proporcionados.");
        }

        var userFilter = Builders<Usuarios>.Filter.Eq(u => u.nombre_usuario, usuario.nombre_usuario);
        var user = await _usuariosCollection.Find(userFilter).FirstOrDefaultAsync();

        if (user == null)
        {
            return Ok("Usuario incorrecto.");
        }

        var passwordFilter = Builders<Usuarios>.Filter.Eq(u => u.nombre_usuario, usuario.nombre_usuario) &
                             Builders<Usuarios>.Filter.Eq(u => u.contrasena, usuario.contrasena);
        var login = await _usuariosCollection.Find(passwordFilter).FirstOrDefaultAsync();

        if (login == null)
        {
            return Ok("Contraseña incorrecta.");
        }

        return Ok(login);
    }

    // PUT api/usuarios/{id}
    [HttpPut]
    [Route("{userId}")]
    public async Task<IHttpActionResult> Put(int userId, [FromBody] Usuarios usuario)
    {
        if (usuario == null)
        {
            return BadRequest("Usuario no proporcionado.");
        }

        var savedId = userId;
        var filter = Builders<Usuarios>.Filter.Eq(u => u.userId, savedId);

        var update = Builders<Usuarios>.Update
            .Set(u => u.nombre, usuario.nombre)
            .Set(u => u.apellido, usuario.apellido)
            .Set(u => u.nombre_usuario, usuario.nombre_usuario)
            .Set(u => u.contrasena, usuario.contrasena)
            .Set(u => u.pic, usuario.pic);

        var result = await _usuariosCollection.UpdateOneAsync(filter, update);

        if (result.MatchedCount == 0)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    // PUT api/usuarios/usuariosPic/{id}
    [HttpPut]
    [Route("usuariosPic/{userId}")]
    public async Task<IHttpActionResult> PutPic(int userId, [FromBody] Usuarios usuario)
    {
        var savedId = userId;
        var filter = Builders<Usuarios>.Filter.Eq(u => u.userId, savedId);

        var update = Builders<Usuarios>.Update
            .Set(u => u.pic, usuario.pic);

        var result = await _usuariosCollection.UpdateOneAsync(filter, update);

        if (result.MatchedCount == 0)
        {
            return NotFound();
        }

        return Ok(usuario);
    }


    // DELETE api/historial/{id}
    [HttpDelete]
    [Route("{userId}")]
    public async Task<IHttpActionResult> Delete(int userId)
    {
        var result = await _usuariosCollection.DeleteOneAsync(x => x.userId == userId);
        if (result.IsAcknowledged && result.DeletedCount > 0)
        {
            return Ok("Usuario eliminado.");
        }
        return BadRequest("Usuario inexistente.");
    }
}


