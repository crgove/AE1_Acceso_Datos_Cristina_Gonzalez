using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class Apuesta
    {
        public Apuesta(int id, bool esUnder, double cuota, double dinero, int idMercado, string emailUsuario)
        {
            Id = id;
            EsUnder = esUnder;
            Cuota = cuota;
            Dinero = dinero;
            IdMercado = idMercado;
            EmailUsuario = emailUsuario;
        }

        public int Id {get; set; }
        public bool EsUnder { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public int IdMercado { get; set; } 
        public string EmailUsuario { get; set; }

    }

    public class ApuestaDTO
    {
        public ApuestaDTO(bool esUnder, double cuota, double dinero, string emailUsuario, double tipoMercado)
        {
            EsUnder = esUnder;
            Cuota = cuota;
            Dinero = dinero;
            EmailUsuario = emailUsuario;
            TipoMercado = tipoMercado;
        }

        public bool EsUnder { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public string EmailUsuario { get; set; }
        public double TipoMercado { get; set; }

    }

    public class ApuestaUsuario
    {
        public ApuestaUsuario(bool esUnder, double cuota, double dinero, double tipoMercado, int idEvento)
        {
            EsUnder = esUnder;
            Cuota = cuota;
            Dinero = dinero;
            TipoMercado = tipoMercado;
            IdEvento = idEvento;
        }

        public bool EsUnder { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public double TipoMercado { get; set; }
        public int IdEvento { get; set; }
    }
}