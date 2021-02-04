using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Clientes> _ClientesCollection;

        public ClientesController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _ClientesCollection = _mongoDB.DB.GetCollection<Clientes>(typeof(Clientes).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarClientes([FromBody] ClientesDto dto)
        {
            var clientes = new Clientes(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _ClientesCollection.InsertOne(clientes);
            
            return StatusCode(201, "Cliente adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterClientes()
        {
            var clientes = _ClientesCollection.Find(Builders<Clientes>.Filter.Empty).ToList();
            
            return Ok(clientes);
        }
    }
}
