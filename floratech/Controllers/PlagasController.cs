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
[RoutePrefix("api/plagas")]
public class PlagasController : ApiController
{
    private readonly IMongoCollection<Plagas> _plagasCollection;

    public PlagasController()
    {
        mongo_db db_mongo = new mongo_db("floratech", "floratech1", "floratech-db", "maincluster.ixylg4p.mongodb.net/");
        _plagasCollection = db_mongo.mongoDatabase.GetCollection<Plagas>("plagas");
    }

    // GET api/plantas
    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<Plagas>> Get()
    {
        return await _plagasCollection.Find(new BsonDocument()).ToListAsync();
    }

    // GET api/plantas/all
    [HttpGet]
    [Route("all")]
    public async Task<IHttpActionResult> GetAllHistorial()
    {
        var plagas = await _plagasCollection
            .Find(_ => true)
            .ToListAsync();

        var totalPlantas = plagas.Count;

        return Ok(new
        {
            plagas,
            totalPlantas
        });
    }

    // GET api/plantas/{pestId}
    [HttpGet]
    [Route("{pestId}")]
    public async Task<IHttpActionResult> GetPlantaByPlantId(int pestId)
    {
        var plaga = await _plagasCollection.Find(x => x.pestId == pestId).FirstOrDefaultAsync();
        if (plaga == null)
        {
            return NotFound();
        }
        return Ok(plaga);
    }
}

