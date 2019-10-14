using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProyectoApi.Models;

namespace WebProyectoApi.Controllers
{
    public class EventosController : ApiController
    {
        // GET: api/Eventos
        /*public IEnumerable<Evento> Get()
        {
            var repo = new EventosRepository();
            return repo.Retrieve();
        }

        */
        public IEnumerable<EventoDTO> Get()  
        {
            var repo = new EventosRepository();
            return repo.RetrieveDTO();
        }

        // GET: api/Eventos/5
        public string Get(int id)
        {
            return null;
        }

        // POST: api/Eventos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Eventos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Eventos/5
        public void Delete(int id)
        {
        }
    }
}
