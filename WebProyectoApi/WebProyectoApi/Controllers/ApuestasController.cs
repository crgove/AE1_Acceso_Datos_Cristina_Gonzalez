﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProyectoApi.Models;

namespace WebProyectoApi.Controllers
{
    public class ApuestasController : ApiController
    {
        // GET: api/Apuestas
        /*public IEnumerable<Apuesta> Get()
        {
            var repo = new ApuestasRepository();
            return repo.Retrieve();
        }*/

        public IEnumerable<ApuestaDTO> Get()
        {
            var repo = new ApuestasRepository();
            return repo.RetrieveDTO();
        }


        // GET: api/Apuestas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Apuestas
        public void Post([FromBody]Apuesta apuesta)
        {
            var repo = new ApuestasRepository();
            repo.Save(apuesta);

            

        }

        // PUT: api/Apuestas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Apuestas/5
        public void Delete(int id)
        {
        }
    }
}
