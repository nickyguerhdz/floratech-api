using floratech.Model;
using floratech.Connection;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

[EnableCors("*", "*", "*")]
[RoutePrefix("api/favoritos")]
public class FavoritosController : ApiController
{
    private readonly IMongoCollection<Favoritos> _favoritosCollection;

    private async Task<int> GetNextFavIdAsync()
    {
        var sort = Builders<Favoritos>.Sort.Descending(u => u.favId);
        var highestFavId = await _favoritosCollection.Find(_ => true).Sort(sort).Limit(1).FirstOrDefaultAsync();

        return highestFavId != null ? highestFavId.favId + 1 : 1;
    }

    public FavoritosController()
    {
        mongo_db db_mongo = new mongo_db("floratech", "floratech1", "floratech-db", "maincluster.ixylg4p.mongodb.net/");

        _favoritosCollection = db_mongo.mongoDatabase.GetCollection<Favoritos>("favoritos");
    }

    // GET api/favoritos/nextFavId
    [HttpGet]
    [Route("nextFavId")]
    public async Task<IHttpActionResult> GetNextFavId()
    {
        try
        {
            int nextFavId = await GetNextFavIdAsync();
            return Ok(nextFavId);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    // GET api/favoritos
    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<Favoritos>> Get()
    {
        return await _favoritosCollection.Find(new BsonDocument()).ToListAsync();
    }

    // GET api/favoritos/{userId}
    [HttpGet]
    [Route("{userId}")]
    public async Task<IHttpActionResult> GetFavoritosByUserId(int userId)
    {
        var favorito = await _favoritosCollection.Find(x => x.userId == userId).ToListAsync();
        if (favorito.Count == 0)
        {
            return Ok(0);
        }
        return Ok(favorito);
    }

    // GET api/favoritos/{userId}/frutas
    [HttpGet]
    [Route("{userId}/frutas")]
    public async Task<IHttpActionResult> GetFrutasFavByUserId(int userId)
    {
        var favorito = await _favoritosCollection.Find(x => x.userId == userId && x.tipo == "Fruta").ToListAsync();
        if (favorito.Count == 0)
        {
            return Ok(0);
        }
        return Ok(favorito);
    }

    // GET api/favoritos/{userId}/plantas
    [HttpGet]
    [Route("{userId}/plantas")]
    public async Task<IHttpActionResult> GetPlantasFavByUserId(int userId)
    {
        var favorito = await _favoritosCollection.Find(x => x.userId == userId && x.tipo == "Planta").ToListAsync();
        if (favorito.Count == 0)
        {
            return Ok(0);
        }
        return Ok(favorito);
    }

    // GET api/favoritos/{userId}/plagas
    [HttpGet]
    [Route("{userId}/plagas")]
    public async Task<IHttpActionResult> GetPlagasFavByUserId(int userId)
    {
        var favorito = await _favoritosCollection.Find(x => x.userId == userId && x.tipo == "Plaga").ToListAsync();
        if (favorito.Count == 0)
        {
            return Ok(0);
        }
        return Ok(favorito);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IHttpActionResult> GetAllFavoritos()
    {
        var favoritos = await _favoritosCollection
            .Find(_ => true)
            .ToListAsync();

        var totalFavoritos = favoritos.Count;

        return Ok(new
        {
            favoritos,
            totalFavoritos
        });
    }

    // POST api/favoritos
    [HttpPost]
    [Route("")]
    public async Task<IHttpActionResult> Post([FromBody] Favoritos favorito)
    {
        if (favorito == null)
        {
            return BadRequest("Favorito incompleto.");
        }

        favorito.favId = await GetNextFavIdAsync();

        await _favoritosCollection.InsertOneAsync(favorito);
        return Ok(favorito);
    }

    // POST api/favoritos/check (Revisar si el fav existe)
    [HttpPost]
    [Route("check")]
    public async Task<IHttpActionResult> CheckFavorito([FromBody] Favoritos favorito)
    {
        var favoritoEncontrado = await _favoritosCollection.Find(x => x.userId == favorito.userId &&
                                                                      x.tipo == favorito.tipo &&
                                                                      x.id_elemento == favorito.id_elemento &&
                                                                      x.elemento == favorito.elemento)
                                                                .FirstOrDefaultAsync();

        bool exists = favoritoEncontrado != null;
        int favId = exists ? favoritoEncontrado.favId : 0;

        var responseObj = new
        {
            Exists = exists,
            FavId = favId
        };

        return Ok(responseObj);
    }

    // POST api/favoritos/checkId (Revisar si el favId existe)
    [HttpPost]
    [Route("checkId")]
    [ResponseType(typeof(bool))]
    public async Task<IHttpActionResult> GetFavoritoId([FromBody] Favoritos favorito)
    {
        var existingFavorito = await _favoritosCollection.Find(x => x.favId == favorito.favId && x.userId == favorito.userId && x.tipo == favorito.tipo && x.id_elemento == favorito.id_elemento && x.elemento == favorito.elemento).FirstOrDefaultAsync();

        return Ok(existingFavorito);
    }

    // DELETE api/favoritos/{favId} (Eliminar por favId)
    [HttpDelete]
    [Route("{favId}")]
    public async Task<IHttpActionResult> Delete(int favId)
    {
        var result = await _favoritosCollection.DeleteOneAsync(x => x.favId == favId);
        if (result.IsAcknowledged && result.DeletedCount > 0)
        {
            return Ok("Favorito eliminado correctamente.");
        }
        return Ok("Favorito inexistente.");
    }
}


