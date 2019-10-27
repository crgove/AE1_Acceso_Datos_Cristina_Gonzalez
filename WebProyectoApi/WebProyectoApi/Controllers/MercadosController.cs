using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProyectoApi.Models;

namespace WebProyectoApi.Controllers
{
    public class MercadosController : ApiController
    {
        // GET: api/Mercados
        /*public IEnumerable<Mercado> Get()
        {
            var repo = new MercadosRepository();
            return repo.Retrieve();
        }*/

        public IEnumerable<MercadoDTO> Get()
        {
            var repo = new MercadosRepository();
            return repo.RetrieveDTO();
        }

        // GET: api/Mercados/5
        /*public string Get(int id)
        {
            return null;
        }*/

         //DEVUELVE LOS MERCADOS DE UN EVENTO EN CONCRETO
        // GET: api/Mercados?EventoId=id
        public IEnumerable<MercadoDTO> GetByIdEvento(int EventoId)
        {
            var repo = new MercadosRepository(); 
            return repo.RetrieveById(EventoId);  
        }

        // POST: api/Mercados
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mercados/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Mercados/5
        public void Delete(int id)
        {
        }
    }
}
