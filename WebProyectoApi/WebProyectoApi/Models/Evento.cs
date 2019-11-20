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

    public class EventoDTO
    {
        public EventoDTO(string equipoLocal, string equipoVisitante)
        {
            EquipoLocal = equipoLocal;
            EquipoVisitante = equipoVisitante;
        }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        
    }

    //PREGUNTA 3 EXAMEN
    public class EventoExamen
    {
        public EventoExamen(int id, string equipoLocal, string equipoVisitante, double dinero)
        {
            Id = id;
            EquipoLocal = equipoLocal;
            EquipoVisitante = equipoVisitante;
            Dinero = dinero;
        }

        public int Id { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }

        public double Dinero { get; set; }

    }
}