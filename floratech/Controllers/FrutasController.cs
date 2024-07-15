using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace floratech.Controllers
{
    [EnableCors("*", "*", "*")]
    public class FruityviceController : ApiController
    {
        private static readonly string BaseUrl = "https://www.fruityvice.com/api/fruit/";

        // Método privado para obtener todos los valores
        private async Task<Newtonsoft.Json.Linq.JArray> GetAllFruitsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("all");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.Linq.JArray.Parse(data);
                }
                else
                {
                    throw new Exception("Error al obtener datos de Fruityvice.");
                }
            }
        }

        // GET: api/fruityvice/{id}
        [HttpGet]
        [Route("api/fruityvice/{id}")]
        public async Task<IHttpActionResult> GetFruitById(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(id);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return Ok(data);
                    }
                    else
                    {
                        return BadRequest("Error al obtener datos de Fruityvice.");
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        // GET: api/fruityvice/name/{name}
        [HttpGet]
        [Route("api/fruityvice/name/{name}")]
        public async Task<IHttpActionResult> GetFruitByName(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"name/{name}");
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return Ok(data);
                    }
                    else
                    {
                        return BadRequest("Error al obtener datos de Fruityvice.");
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        // GET: api/fruityvice/sort/name/{order}
        [HttpGet]
        [Route("api/fruityvice/sort/name/{order}")]
        public async Task<IHttpActionResult> GetFruitsSortedByName(string order)
        {
            try
            {
                var fruits = await GetAllFruitsAsync();

                if (order.ToLower() == "asc")
                {
                    fruits = new Newtonsoft.Json.Linq.JArray(fruits.OrderBy(f => (string)f["name"]));
                }
                else if (order.ToLower() == "desc")
                {
                    fruits = new Newtonsoft.Json.Linq.JArray(fruits.OrderByDescending(f => (string)f["name"]));
                }
                else
                {
                    return BadRequest("El orden debe ser 'asc' o 'desc'.");
                }

                return Ok(fruits);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/fruityvice/sort/id/{order}
        [HttpGet]
        [Route("api/fruityvice/sort/id/{order}")]
        public async Task<IHttpActionResult> GetFruitsSortedById(string order)
        {
            try
            {
                var fruits = await GetAllFruitsAsync();

                if (order.ToLower() == "asc")
                {
                    fruits = new Newtonsoft.Json.Linq.JArray(fruits.OrderBy(f => (int)f["id"]));
                }
                else if (order.ToLower() == "desc")
                {
                    fruits = new Newtonsoft.Json.Linq.JArray(fruits.OrderByDescending(f => (int)f["id"]));
                }
                else
                {
                    return BadRequest("El orden debe ser 'asc' o 'desc'.");
                }

                return Ok(fruits);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

