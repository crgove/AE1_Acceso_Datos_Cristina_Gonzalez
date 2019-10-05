using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class Evento
    {
        public Evento(int id, string equipoLocal, string equipoVisitante, DateTime inicioPartido, DateTime finalPartido)
        {
            Id = id;
            EquipoLocal = equipoLocal;
            EquipoVisitante = equipoVisitante;
            InicioPartido = inicioPartido;
            FinalPartido = finalPartido;
        }

        public int Id { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public DateTime InicioPartido { get; set; }
        public DateTime FinalPartido { get; set; }
    }
}