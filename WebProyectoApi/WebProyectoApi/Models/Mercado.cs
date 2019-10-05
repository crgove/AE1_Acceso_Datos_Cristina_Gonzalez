using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProyectoApi.Models
{
    public class Mercado
    {
        public Mercado(int id, string tipo, double cuotaOver, double cuotaUnder, double dineroApostadoOver, double dineroApostadoUnder, int idEvento)
        {
            Id = id;
            Tipo = tipo;
            CuotaOver = cuotaOver;
            CuotaUnder = cuotaUnder;
            DineroApostadoOver = dineroApostadoOver;
            DineroApostadoUnder = dineroApostadoUnder;
            IdEvento = idEvento;
        }

        public int Id { get; set; }
        public string Tipo { get; set; }
        public double CuotaOver { get; set; }
        public double CuotaUnder { get; set; }
        public double DineroApostadoOver { get; set; }
        public double DineroApostadoUnder { get; set; }
        public int IdEvento { get; set; }
    }
}