using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //DEVUELVE LAS APUESTAS DE UN USUARIO EN CONCRETO
        // GET: api/Apuestas?email=email
        public IEnumerable<ApuestaUsuario> GetByEmail(string email)
        {
            var repo = new ApuestasRepository();
            return repo.RetrieveByEmail(email);
        }

        //DEVUELVE LAS APUESTAS DE UN MERCADO EN CONCRETO
        [Authorize (Roles = "Admin")]
        // GET: api/Apuestas?MercadoId=id
        public IEnumerable<ApuestaDTO> GetByMercado(int MercadoId)
        {
            var repo = new ApuestasRepository();
            return repo.RetrieveByApuesta(MercadoId);
            
        }

        [Authorize]    //Restringimos realizar apuestas solo para usuarios que se han logueado y autenticado!
        // POST: api/Apuestas
        public void Post([FromBody]Apuesta apuesta)
        {
            //Debug.WriteLine("Apuesta vale" + apuesta);
            var repoApuestas = new ApuestasRepository(); 
            repoApuestas.Save(apuesta); //Insertamos la apuesta en la BBDD

            var repoMercados = new MercadosRepository(); 
            repoApuestas.ModificarDineroEsUnderMercado(apuesta); //Llamamos a la función que modifica el dinero(under o over) del mercado una vez se inserta una apuesta
            repoMercados.ActualizarMercado(apuesta); //Actualizamos el Mercado a través de la apuesta insertada
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
