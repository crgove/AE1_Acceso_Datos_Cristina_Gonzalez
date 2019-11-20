using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProyectoApi.Models;

namespace WebProyectoApi.Controllers
{
    public class ApuestasExamenController : ApiController
    {

        //EJERCICIO 1 EXAMEN
        public IEnumerable<ApuestaExamen> Get()
        {
            var repo = new ApuestasExamenRepository();
            return repo.Retrieve();
        }

        // GET: api/ApuestasExamen
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/ApuestasExamen/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApuestasExamen
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApuestasExamen/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApuestasExamen/5
        public void Delete(int id)
        {
        }
    }
}
