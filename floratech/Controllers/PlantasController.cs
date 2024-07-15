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
[RoutePrefix("api/plantas")]
public class PlantasController : ApiController
{
    private readonly IMongoCollection<Plantas> _plantasCollection;

    public PlantasController()
    {
        mongo_db db_mongo = new mongo_db("floratech", "floratech1", "floratech-db", "maincluster.ixylg4p.mongodb.net/");
        _plantasCollection = db_mongo.mongoDatabase.GetCollection<Plantas>("plantas");
    }

    // GET api/plantas
    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<Plantas>> Get()
    {
        return await _plantasCollection.Find(new BsonDocument()).ToListAsync();
    }
 
    // GET api/plantas/all
    [HttpGet]
    [Route("all")]
    public async Task<IHttpActionResult> GetAllHistorial()
    {
        var plantas = await _plantasCollection
            .Find(_ => true)
            .ToListAsync();

        var totalPlantas = plantas.Count;

        return Ok(new
        {
            plantas,
            totalPlantas
        });
    }

    // GET api/plantas/{plantId}
    [HttpGet]
    [Route("{plantId}")]
    public async Task<IHttpActionResult> GetPlantaByPlantId(int plantId)
    {
        var planta = await _plantasCollection.Find(x => x.plantId == plantId).FirstOrDefaultAsync();
        if (planta == null)
        {
            return NotFound();
        }
        return Ok(planta);
    }
}
