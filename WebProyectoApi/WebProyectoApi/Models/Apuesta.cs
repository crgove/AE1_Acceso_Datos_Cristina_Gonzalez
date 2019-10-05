using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class Apuesta
    {
        public Apuesta(int id, string tipo, double cuota, double dinero, int idMercado, string emailUsuario)
        {
            Id = id;
            Tipo = tipo;
            Cuota = cuota;
            Dinero = dinero;
            IdMercado = idMercado;
            EmailUsuario = emailUsuario;
        }

        public int Id {get; set; }
        public string Tipo { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public int IdMercado { get; set; } 
        public string EmailUsuario { get; set; }

    }
}