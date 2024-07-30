using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace floratech.Model
{
    public class Historial
    {
        public Historial()
        {
            userId = 0;
            busqueda = "Búsqueda vacía";
            fecha = DateTime.Now;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("userId")]
        public int userId { get; set; }

        [BsonElement("busqueda")]
        public string busqueda { get; set; }

        [BsonElement("fecha")]
        public DateTime fecha { get; set; }

    }

    public class Plantas
    {
        public Plantas()
        {
            plantId = 0;
            botanical_name = "N/A";
            common_name = "N/A";
            description = "N/A";
            genus = "N/A";
            family = "N/A";
            care_type = "N/A";
            watering = "N/A";
            sunlight = "N/A";
            start_seeds = "N/A";
            harvest_crops = "N/A";
            seed_spacing = "N/A";
            plant_type = "N/A";
            mature_size = "N/A";
            soil_type = "N/A";
            soil_ph = "N/A";
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("plantId")]
        public int plantId { get; set; }

        [BsonElement("botanical_name")]
        public string botanical_name { get; set; }

        [BsonElement("common_name")]
        public string common_name { get; set; }

        [BsonElement("description")]
        public string description { get; set; }

        [BsonElement("genus")]
        public string genus { get; set; }

        [BsonElement("family")]
        public string family { get; set; }

        [BsonElement("care_type")]
        public string care_type { get; set; }

        [BsonElement("watering")]
        public string watering { get; set; }

        [BsonElement("sunlight")]
        public string sunlight { get; set; }

        [BsonElement("start_seeds")]
        public string start_seeds { get; set; }

        [BsonElement("harvest_crops")]
        public string harvest_crops { get; set; }

        [BsonElement("seed_spacing")]
        public string seed_spacing { get; set; }

        [BsonElement("plant_type")]
        public string plant_type { get; set; }

        [BsonElement("mature_size")]
        public string mature_size { get; set; }

        [BsonElement("soil_type")]
        public string soil_type { get; set; }

        [BsonElement("soil_ph")]
        public string soil_ph { get; set; }

    }

    public class Plagas
    {
        public Plagas()
        {
            pestId = 0;
            botanical_name = "N/A";
            common_name = "N/A";
            description = "N/A";
            genus = "N/A";
            family = "N/A";
            plants_affected = "N/A";
            actions = "N/A";
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("pestId")]
        public int pestId { get; set; }

        [BsonElement("botanical_name")]
        public string botanical_name { get; set; }

        [BsonElement("common_name")]
        public string common_name { get; set; }

        [BsonElement("description")]
        public string description { get; set; }

        [BsonElement("genus")]
        public string genus { get; set; }

        [BsonElement("family")]
        public string family { get; set; }

        [BsonElement("plants_affected")]
        public string plants_affected { get; set; }

        [BsonElement("actions")]
        public string actions { get; set; }

    }

    public class Usuarios
    {
        public Usuarios()
        {
            nombre = "N/A";
            apellido = "N/A";
            nombre_usuario = "N/A";
            contrasena = "N/A";
            pic = "mar";
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("userId")]
        public int userId { get; set; }

        [BsonElement("nombre")]
        public string nombre { get; set; }

        [BsonElement("apellido")]
        public string apellido { get; set; }

        [BsonElement("nomusuario")]
        public string nombre_usuario { get; set; }

        [BsonElement("contrasena")]
        public string contrasena { get; set; }

        [BsonElement("pic")]
        public string pic { get; set; }
    }

    public class Favoritos
    {
        public Favoritos()
        {
            userId = 0;
            tipo = "N/A";
            id_elemento = 0;
            elemento = "N/A";
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("userId")]
        public int userId { get; set; }

        [BsonElement("favId")]
        public int favId { get; set; }

        [BsonElement("tipo")]
        public string tipo { get; set; }

        [BsonElement("id_elemento")]
        public int id_elemento { get; set; }

        [BsonElement("elemento")]
        public string elemento { get; set; }

    }

    public class FavoritoResponse
    {
        public bool Exists { get; set; }
    }
}

