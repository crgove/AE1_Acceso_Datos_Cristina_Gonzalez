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

        //PREGUNTA 3 EXAMEN
        // POST: api/Eventos
        public void Post([FromBody]Evento evento)
        {
            //Debug.WriteLine("Apuesta vale" + apuesta);
            var repoEventos = new EventosRepository();
            repoEventos.Save(evento); //Insertamos EL EVENTO en la BBDD

            //RECUPERAMOS EL MERCADO DEL EVENTO ESPECIFICO
            var repoMercados = new MercadosRepository();
            repoMercados.RetrieveById(evento.Id);
            

            //ME FALTA MUCHOOOO
            
            
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
