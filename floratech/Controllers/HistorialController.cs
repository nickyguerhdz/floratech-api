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
[RoutePrefix("api/historial")]
public class HistorialController : ApiController
{
    private readonly IMongoCollection<Historial> _historialCollection;

    public HistorialController()
    {
        mongo_db db_mongo = new mongo_db("floratech", "floratech1", "floratech-db", "maincluster.ixylg4p.mongodb.net/");

        _historialCollection = db_mongo.mongoDatabase.GetCollection<Historial>("historial");
    }

    // GET api/historial
    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<Historial>> Get()
    {
        return await _historialCollection.Find(new BsonDocument()).ToListAsync();
    }

    // GET api/historial/user/{userId}
    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IHttpActionResult> GetHistorialByUserId(int userId)
    {
        var historial = await _historialCollection.Find(x => x.userId == userId).ToListAsync();
        if (historial == null)
        {
            return Ok(new List<Historial>());
        }
        return Ok(historial);
    }


    // GET api/historial/{userId}/last5
    [HttpGet]
    [Route("{userId}/last5/")]
    public async Task<IHttpActionResult> GetLast5Historial(int userId)
    {
        var last5Historial = await _historialCollection
            .Find(x => x.userId == userId)
            .SortByDescending(x => x.fecha)
            .Limit(5)
            .ToListAsync();

        return Ok(last5Historial);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IHttpActionResult> GetAllHistorial()
    {
        var historial = await _historialCollection
            .Find(_ => true)
            .ToListAsync();

        var totalHistorial = historial.Count;

        return Ok(new
        {
            historial,
            totalHistorial
        });
    }

    // POST api/historial
    [HttpPost]
    [Route("")]
    public async Task<IHttpActionResult> Post([FromBody] Historial historial)
    {
        if (historial == null)
        {
            return Ok((Historial)null);
        }

        await _historialCollection.InsertOneAsync(historial);
        return Ok(historial);
    }

    // DELETE api/historial/{id}
    [HttpDelete]
    [Route("{id}")]
    public async Task<IHttpActionResult> Delete(string id)
    {
        var result = await _historialCollection.DeleteOneAsync(x => x.Id == new ObjectId(id));
        if (result.IsAcknowledged && result.DeletedCount > 0)
        {
            return Ok();
        }
        return NotFound();
    }
}

